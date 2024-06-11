namespace EventSourcingExample.Events
{
    public class MoneyDeposited : IEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }

        public MoneyDeposited(Guid accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
