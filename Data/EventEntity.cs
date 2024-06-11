using System.ComponentModel.DataAnnotations;

using EventSourcingExample.Events;

using Newtonsoft.Json;

namespace EventSourcingExample.Data
{
    public class EventEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AggregateId { get; set; }

        [Required]
        public DateTime OccurredOn { get; set; }

        [Required]
        public string EventType { get; set; }

        [Required]
        public string Data { get; set; }

        public static EventEntity FromEvent(IEvent @event)
        {
            return new EventEntity
            {
                Id = @event.Id,
                AggregateId = @event is AccountCreated accountCreated ? accountCreated.AccountId : @event is MoneyDeposited deposited ? deposited.AccountId : ((MoneyWithdrawn)@event).AccountId,
                OccurredOn = @event.OccurredOn,
                EventType = @event.GetType().AssemblyQualifiedName,
                Data = JsonConvert.SerializeObject(@event)
            };
        }

        public IEvent ToEvent()
        {
            var eventType = Type.GetType(EventType);
            return (IEvent)JsonConvert.DeserializeObject(Data, eventType);
        }
    }
}