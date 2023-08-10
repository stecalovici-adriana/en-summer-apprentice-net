using Microsoft.EntityFrameworkCore;
using TicketManagerSystem.Api.Exceptions;
using TicketManagerSystem.Api.Models;

namespace TicketManagerSystem.Api.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagerSystemContext _dbContext;

        public EventRepository()
        {
            _dbContext = new TicketManagerSystemContext();
        }
        public int Add(Event @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events
                .Include(e => e.EventType)
                .Where(e => e.EventTypeId != null)
                .Include(e => e.Venue)
                .Where(e => e.VenueId != null)
                .ToList();

            return events;
        }

        public async Task<Event> GetByEventId(int id)
        {
            var @event = await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();

            if (@event == null)
                throw new EntityNotFoundException(id, nameof(Event));

            return @event;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
