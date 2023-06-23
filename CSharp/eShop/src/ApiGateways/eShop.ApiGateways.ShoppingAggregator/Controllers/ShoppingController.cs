using eShop.ApiGateways.ShoppingAggregator.Models;
using eShop.ApiGateways.ShoppingAggregator.Services;

using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace eShop.ApiGateways.ShoppingAggregator.Controllers
{
    [ApiController]
    [Route("api/v1/shopping")]
    public class ShoppingController : ControllerBase
    {
        private readonly IBasketService _basketService;

        private readonly IOrderingService _orderingService;

        private readonly ICatalogService _catalogService;

        public ShoppingController(
            IBasketService basketService,
            IOrderingService orderingService,
            ICatalogService catalogService)
        {
            _basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            _orderingService = orderingService ?? throw new ArgumentNullException(nameof(orderingService));
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
        }

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(ShoppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingModel>> GetShopping([FromRoute] string userName)
        {
            var basket = await _basketService.GetBasket(userName);

            foreach (var item in basket.Items)
            {
                var product = await _catalogService.GetCatalog(item.ProductId);

                item.ProductName = product.Name;
                item.Category = product.Category;
                item.Summary = product.Summary;
                item.Description = product.Description;
                item.ImageFile = product.ImageFile;
            }

            var orders = await _orderingService.GetOrdersByUserName(userName);

            var shoppingModel = new ShoppingModel
            {
                Username = userName,
                Basket = basket,
                Orders = orders,
            };

            return Ok(shoppingModel);
        }
    }
}
