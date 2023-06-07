using eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder;
using eShop.Ordering.Application.Features.Order.Commands.DeleteOrder;
using eShop.Ordering.Application.Features.Order.Commands.UpdateOrder;
using eShop.Ordering.Application.Features.Order.Queries.GetOrdersList;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace eShop.Ordering.Api.Controllers
{
    [ApiController]
    [Route("api/v1/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{username}")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromRoute] string username)
        {
            var allOrders = await _mediator.Send(new GetOrdersListQuery(username));

            if (allOrders?.Any() == false)
            {
                return NotFound();
            }

            return Ok(allOrders);
        }

        // This is for testing purposes. This feature will be called from a RabbitMQ message.
        [HttpPost]
        [Route("{username}/checkout")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromBody] CheckoutOrderCommand command)
        {
            var result = await _mediator.Send(command);

            if (result < 1)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut]
        [Route("{username}")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromBody] UpdateOrderCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [Route("{orderId}")]
        [ProducesResponseType(typeof(IEnumerable<OrderDto>), (int)HttpStatusCode.NoContent)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll([FromRoute] int orderId)
        {
            await _mediator.Send(new DeleteOrderCommand() { Id = orderId });

            return NoContent();
        }
    }
}
