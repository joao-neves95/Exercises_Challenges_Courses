using eShop.Ordering.Application.Contracts.Infrastructure;
using eShop.Ordering.Application.Models;

namespace eShop.Ordering.Infrastructure.Infrastructure
{
    public sealed class EmailService : IEmailService
    {
        public Task<bool> SendEmail(Email email)
        {
            throw new NotImplementedException();
        }
    }
}
