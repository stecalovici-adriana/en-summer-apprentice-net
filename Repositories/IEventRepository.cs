using TicketManagerSystem.Api.Models;

namespace TicketManagerSystem.Api.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Task<Event> GetById(int id);

        int Add(Event @event);

        void Update(Event @event);

        void Delete(Event @event);
    }
}
