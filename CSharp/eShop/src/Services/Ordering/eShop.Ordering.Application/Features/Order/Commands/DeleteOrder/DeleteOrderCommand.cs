using MediatR;

namespace eShop.Ordering.Application.Features.Order.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}
