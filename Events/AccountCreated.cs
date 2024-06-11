namespace EventSourcingExample.Events
{
    public class AccountCreated : IEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public Guid AccountId { get; set; }
        public string Owner { get; set; }

        public AccountCreated(Guid accountId, string owner)
        {
            AccountId = accountId;
            Owner = owner;
        }
    }
}
