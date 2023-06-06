using eShop.Ordering.Application.Contracts.Infrastructure;
using eShop.Ordering.Application.Contracts.Persistence;
using eShop.Ordering.Application.Models;
using eShop.Ordering.Domain.Entities;

using AutoMapper;
using MediatR;

using Microsoft.Extensions.Logging;

namespace eShop.Ordering.Application.Features.Order.Commands.CheckoutOrder
{
    internal sealed class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;

        private readonly IMapper _mapper;

        private readonly IEmailService _emailService;

        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            IEmailService emailService,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var mappedOrderEntity = _mapper.Map<DataOrder>(request);
            var insertedOrder = await _orderRepository.AddAsync(mappedOrderEntity);

            _logger.LogInformation($"Order {insertedOrder.Id} was successfully created.");

            await SendMail(insertedOrder);

            return insertedOrder.Id;
        }

        private async Task SendMail(DataOrder order)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = $"Order was created.", Subject = "Order was created" };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
