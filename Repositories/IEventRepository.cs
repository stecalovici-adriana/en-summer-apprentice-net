using TicketManagerSystem.Api.Models;

namespace TicketManagerSystem.Api.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Task<Event> GetByEventId(int id);

        int Add(Event @event);

        void Update(Event @event);

        void Delete(Event @event);
    }
}
