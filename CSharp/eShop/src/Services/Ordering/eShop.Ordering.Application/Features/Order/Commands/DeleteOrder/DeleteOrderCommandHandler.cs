using eShop.Ordering.Application.Contracts.Persistence;
using eShop.Ordering.Application.Exceptions;
using eShop.Ordering.Domain.Entities;

using Microsoft.Extensions.Logging;

using AutoMapper;
using MediatR;

namespace eShop.Ordering.Application.Features.Order.Commands.DeleteOrder
{
    internal sealed class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            ILogger<DeleteOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(request.Id);

            if (existingOrder == null)
            {
                _logger.LogError($"Order ID={request.Id} does not exist.");
                throw new NotFoundApplicationException(nameof(DataOrder), request.Id);
            }

            await _orderRepository.DeleteAsync(existingOrder);
            _logger.LogInformation($"Order ID={request.Id} successfully deleted.");
        }
    }
}
