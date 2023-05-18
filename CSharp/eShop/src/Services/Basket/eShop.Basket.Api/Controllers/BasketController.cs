using System.Net;

using eShop.Basket.Api.Entities;
using eShop.Basket.Api.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace eShop.Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpGet]
        [Route("{username:string}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetAsync([FromRoute] string username)
        {
            if (username is null)
            {
                return BadRequest("username is null");
            }

            var basket = await _basketRepository.GetAsync(username);

            return Ok(basket ?? new ShoppingCart(username));
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> PostAsync([FromBody] ShoppingCart basket)
        {
            return await PutAsync(basket);
        }

        [HttpPut]
        [Route("")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> PutAsync([FromBody] ShoppingCart basket)
        {
            return Ok(await _basketRepository.UpdateAsync(basket));
        }

        [HttpDelete]
        [Route("")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<bool>> DeleteAsync([FromBody] ShoppingCart basket)
        {
            var result = await _basketRepository.UpdateAsync(basket);

            return result is null
                ? StatusCode((int)HttpStatusCode.InternalServerError)
                : Ok(result);
        }
    }
}
