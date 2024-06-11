using EventSourcingExample.Aggregates;

namespace EventSourcingExample.Repositories
{
    public class BankAccountRepository
    {
        private readonly EventStore _eventStore;

        public BankAccountRepository(EventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task SaveAsync(BankAccount bankAccount)
        {
            var changes = bankAccount.GetUncommittedChanges();
            await _eventStore.SaveEventsAsync(bankAccount.Id, changes);
            bankAccount.MarkChangesAsCommitted();
        }

        public async Task<BankAccount> GetByIdAsync(Guid accountId)
        {
            var events = await _eventStore.GetEventsAsync(accountId);
            var bankAccount = new BankAccount();
            foreach (var @event in events)
            {
                bankAccount.Apply(@event);
            }
            return bankAccount;
        }
    }
}