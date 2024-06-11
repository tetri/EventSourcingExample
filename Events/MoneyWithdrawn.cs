namespace EventSourcingExample.Events
{
    public class MoneyWithdrawn : IEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public MoneyWithdrawn(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
