using eShop.Discount.Shared.Entities;
using eShop.Discount.Shared.Repositories;

using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace eShop.Discount.Api.Controllers
{
    [ApiController]
    [Route("api/v1/discount")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(IEnumerable<Coupon>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetAll()
        {
            return Ok(await _discountRepository.GetAll());
        }

        [HttpGet]
        [Route("{productName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Get([FromRoute] string productName)
        {
            return Ok(await _discountRepository.Get(productName));
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Post([FromBody] Coupon coupon)
        {
            var created = await _discountRepository.Create(coupon);

            if (!created)
            {
                return BadRequest();
            }

            return Ok(created);
        }

        [HttpPut]
        [Route("{productName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Put([FromBody] Coupon coupon)
        {
            var updated = await _discountRepository.Update(coupon);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok(updated);
        }

        [HttpDelete]
        [Route("{productName}")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> Delete([FromRoute] string productName)
        {
            var deleted = await _discountRepository.Delete(productName);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(deleted);
        }
    }
}
