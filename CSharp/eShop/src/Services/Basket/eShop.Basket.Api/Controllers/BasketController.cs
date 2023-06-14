using eShop.Basket.Api.Entities;
using eShop.Basket.Api.Repositories;
using eShop.Basket.Api.Services;
using eShop.Shared.EventBus.Messages.Events;

using System.Net;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using MassTransit;

namespace eShop.Basket.Api.Controllers
{
    [ApiController]
    [Route("api/v1/basket")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _eventBus;

        public BasketController(
            IBasketRepository basketRepository,
            IDiscountService discountService,
            IMapper mapper,
            IPublishEndpoint eventBus)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet]
        [Route("{username}")]
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
            foreach (var basketItem in basket.Items)
            {
                var discount = (await _discountService.GetCouponAsync(basketItem.ProductName)).Amount;
                basketItem.Price -= discount;
            }

            return Ok(await _basketRepository.UpdateAsync(basket));
        }

        [HttpDelete]
        [Route("{username}")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<bool>> DeleteAsync([FromRoute] string username)
        {
            var result = await _basketRepository.DeleteAsync(username);

            return !result
                ? NotFound(result)
                : Ok(result);
        }

        [HttpPost]
        [Route("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CheckoutAsync([FromBody] BasketCheckout basketRequest)
        {
            if (basketRequest == null)
            {
                return BadRequest();
            }

            var existingBasket = await _basketRepository.GetAsync(basketRequest.UserName);

            if (existingBasket == null)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketRequest);
            eventMessage.TotalPrice = existingBasket.TotalPrice;

            await _eventBus.Publish(eventMessage);

            await _basketRepository.DeleteAsync(basketRequest.UserName);

            return Accepted();
        }
    }
}
