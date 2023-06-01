using AutoMapper;

using eShop.Ordering.Application.Contracts.Persistence;

using MediatR;

namespace eShop.Ordering.Application.Features.Order.Queries.GetOrdersList
{
    internal class GetOrdersListQueryHandler : IRequestHandler<GetOrdersListQuery, IEnumerable<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersListQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<OrderDto>> Handle(
            GetOrdersListQuery request,
            CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersByUserName(request.Username);

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }
    }
}
