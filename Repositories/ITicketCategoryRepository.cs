namespace TicketManagerSystem.Api.Repositories
{
    public interface ITicketCategoryRepository
    {
        decimal GetPriceByTicketCategoryId(int id);
    }
}
