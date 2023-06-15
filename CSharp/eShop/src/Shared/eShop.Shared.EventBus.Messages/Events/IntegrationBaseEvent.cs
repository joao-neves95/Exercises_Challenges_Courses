
namespace eShop.Shared.EventBus.Messages.Events
{
    public abstract class IntegrationBaseEvent
    {
        public Guid Id { get; } = Guid.NewGuid();

        public DateTime CreationDate { get; } = DateTime.UtcNow;
    }
}
