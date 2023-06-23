using AutoMapper;

using eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder;
using eShop.Shared.EventBus.Messages.Events;

using MassTransit;

using MediatR;

namespace eShop.Ordering.Api.EventBusConsumers
{
    public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;

        private readonly IMapper _mapper;

        public BasketCheckoutConsumer(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = _mapper.Map<CheckoutOrderCommand>(context.Message);

            await _mediator.Send(command);
        }
    }
}
