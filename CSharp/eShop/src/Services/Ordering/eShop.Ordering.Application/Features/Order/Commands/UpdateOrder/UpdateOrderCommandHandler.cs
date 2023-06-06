using eShop.Ordering.Application.Contracts.Persistence;
using eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder;
using eShop.Ordering.Domain.Entities;

using Microsoft.Extensions.Logging;

using AutoMapper;
using MediatR;

namespace eShop.Ordering.Application.Features.Order.Commands.UpdateOrder
{
    internal sealed class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.Id);

            if (order == null)
            {
                _logger.LogInformation($"Order {request.Id} does not exist.");
                // TODO: throw
            }

            _mapper.Map(request, order, typeof(UpdateOrderCommand), typeof(DataOrder));

            await _orderRepository.UpdateAsync(order);

            _logger.LogInformation($"Order {request.Id} was successfully updated.");
        }
    }
}
