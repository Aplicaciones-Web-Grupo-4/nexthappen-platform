using Microsoft.EntityFrameworkCore;
using nexthappen_backend.CreateEvent.Domain.Entities;
using nexthappen_backend.ManageEvent.Domain;
using nexthappen_backend.Shared.Infrastructure.Persistence.EFC.Configuration;

namespace nexthappen_backend.ManageEvent.Infrastructure.Repositories;

public class ManageEventRepository : IManageEventRepository
{
    private readonly AppDbContext _context;

    public ManageEventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _context.Events.ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _context.Events.FindAsync(id);
    }

    public async Task UpdateAsync(Event updatedEvent)
    {
        _context.Events.Update(updatedEvent);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteByIdAsync(Guid id)
    {
        await _context.Database.ExecuteSqlRawAsync(
            "DELETE FROM Events WHERE Id = {0}", id);
    }
}