using EventSourcingExample.Data;
using EventSourcingExample.Events;

using Microsoft.EntityFrameworkCore;

namespace EventSourcingExample.Repositories
{
    public class EventStore
    {
        private readonly AppDbContext _context;

        public EventStore(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                var eventEntity = EventEntity.FromEvent(@event);
                _context.Events.Add(eventEntity);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<IEvent>> GetEventsAsync(Guid aggregateId)
        {
            var eventEntities = await _context.Events
                .Where(e => e.AggregateId == aggregateId)
                .ToListAsync();
            return eventEntities.Select(e => e.ToEvent()).ToList();
        }
    }


}
