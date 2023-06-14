
namespace eShop.Shared.EventBus.Messages.Events
{
    public abstract class IntegrationBaseEvent
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
