using TicketManagerSystem.Api.Models;

namespace TicketManagerSystem.Api.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();

        Event GetById(int id);

        int Add(Event @event);

        void Update(Event @event);

        int Delete(int id);
    }
}
