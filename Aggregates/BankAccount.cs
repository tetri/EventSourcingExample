namespace EventSourcingExample.Aggregates
{
    using System.Collections.Generic;

    using EventSourcingExample.Events;

    public class BankAccount
    {
        public Guid Id { get; private set; }
        public string Owner { get; private set; }
        public decimal Balance { get; private set; }
        private List<IEvent> _changes = new List<IEvent>();

        public IEnumerable<IEvent> GetUncommittedChanges() => _changes;

        public void MarkChangesAsCommitted() => _changes.Clear();

        public void Apply(IEvent @event)
        {
            switch (@event)
            {
                case AccountCreated e:
                    Id = e.AccountId;
                    Owner = e.Owner;
                    Balance = 0;
                    break;
                case MoneyDeposited e:
                    Balance += e.Amount;
                    break;
                case MoneyWithdrawn e:
                    Balance -= e.Amount;
                    break;
            }
        }

        public void CreateAccount(Guid accountId, string owner)
        {
            var @event = new AccountCreated(accountId, owner);
            Apply(@event);
            _changes.Add(@event);
        }

        public void DepositMoney(decimal amount)
        {
            var @event = new MoneyDeposited(Id, amount);
            Apply(@event);
            _changes.Add(@event);
        }

        public void WithdrawMoney(decimal amount)
        {
            if (Balance < amount)
                throw new InvalidOperationException("Insufficient funds");

            var @event = new MoneyWithdrawn(Id, amount);
            Apply(@event);
            _changes.Add(@event);
        }

        public decimal GetBalance()
        {
            return Balance;
        }
    }
}
