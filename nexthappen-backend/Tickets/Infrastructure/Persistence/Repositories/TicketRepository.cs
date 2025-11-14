using Microsoft.EntityFrameworkCore;
using nexthappen_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using nexthappen_backend.Tickets.Domain.Entities;

namespace nexthappen_backend.Tickets.Infrastructure.Persistence.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ticket ticket)
    {
        await _context.Set<Ticket>().AddAsync(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task<Ticket?> GetByIdAsync(int ticketId)
        => await _context.Set<Ticket>().FirstOrDefaultAsync(t => t.Id == ticketId);

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(int userId)
        => await _context.Set<Ticket>().Where(t => t.UserId == userId).ToListAsync();

    public Task<bool> ExistsAsync(int ticketId)
        => _context.Set<Ticket>().AnyAsync(t => t.Id == ticketId);

    public async Task RemoveAsync(Ticket ticket)
    {
        _context.Set<Ticket>().Update(ticket);
        await _context.SaveChangesAsync();
    }
}