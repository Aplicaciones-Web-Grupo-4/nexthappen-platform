namespace nexthappen_backend.Tickets.Domain.Entities;

public interface ITicketRepository
{
    Task AddAsync(Ticket ticket);
    Task<Ticket?> GetByIdAsync(int ticketId);
    Task<IEnumerable<Ticket>> GetByUserIdAsync(int userId);
    Task<bool> ExistsAsync(int ticketId);
    Task RemoveAsync(Ticket ticket);
}