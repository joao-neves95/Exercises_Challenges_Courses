using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using WebServer.Services;
using WebServer.Lib.Data;
using WebServer.DataAccess;
using WebServer.Models;

namespace WebServer.Controllers
{
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly MySqlConnection _db;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsersController(IConfiguration configuration, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _db = MySqlObjects.Create();
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return Json(UsersTbl.GetAllUsers(_db));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Json(Utils.GetErrorsFromModelState(ModelState)));

            IList<Dictionary<string, object>> existentUser = await UsersTbl.GetUserByEmailAsync(_db, model.Email);
            if (existentUser.Count > 0)
                return BadRequest(Json(new ErrorUsernameExists()));
            else
            {
                IdentityUser user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                IdentityResult createSuccess = await _userManager.CreateAsync(user, model.Password);
                if (!createSuccess.Succeeded)
                {
                    Dictionary<string, List<string>> error = new Dictionary<string, List<string>>
                    {
                        { "Error", new List<string>() }
                    };

                    foreach (IdentityError err in createSuccess.Errors)
                    {
                        error["Error"].Add(err.Description);
                    }

                    if (error["Error"].Count == 0)
                         return Json(new ErrorUnknown());
                    return BadRequest(Json(error));
                }

                await _signInManager.SignInAsync(user, false);
                return Ok(Json(GenerateJwtToken(model.Email, user)));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(Json(Utils.GetErrorsFromModelState(ModelState)));

            Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (!loginResult.Succeeded)
                return BadRequest(Json(new ErrorLogin()));

            IList<Dictionary<string, object>> appUser = await UsersTbl.GetUserByEmailAsync(_db, model.Email);
            Response.Redirect("/app");
            return Ok(Json(GenerateJwtToken(model.Email, Utils.GetObject<IdentityUser>(appUser[0]))));
        }

        public IActionResult NotAuthorized()
        {
            return Redirect("/");
        }

        //public IActionResult Logout()
        //{

        //}

        private string GenerateJwtToken(string email, IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DotNetEnv.Env.GetString("JWT_KEY")));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);
            DateTime expires = DateTime.UtcNow.AddDays(DotNetEnv.Env.GetDouble("JWT_EXPIRE_DAYS"));

            JwtSecurityToken token = new JwtSecurityToken(
                DotNetEnv.Env.GetString("JWT_ISSUER"),
                DotNetEnv.Env.GetString("JWT_ISSUER"),
                claims,
                expires: expires,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
