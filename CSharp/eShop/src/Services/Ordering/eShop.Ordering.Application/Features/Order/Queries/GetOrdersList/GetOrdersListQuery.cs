using MediatR;

namespace eShop.Ordering.Application.Features.Order.Queries.GetOrdersList
{
    public sealed class GetOrdersListQuery : IRequest<IEnumerable<OrderDto>>
    {
        public string Username { get; set; }

        public GetOrdersListQuery(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException($"'{nameof(username)}' cannot be null or empty.", nameof(username));
            }

            Username = username;
        }
    }
}
