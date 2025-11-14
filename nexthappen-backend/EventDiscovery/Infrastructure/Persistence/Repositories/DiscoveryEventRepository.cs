using nexthappen_backend.EventDiscovery.Domain.Entities;
using nexthappen_backend.EventDiscovery.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using nexthappen_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using nexthappen_backend.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace nexthappen_backend.EventDiscovery.Infrastructure.Persistence.Repositories;
    
public class DiscoveryEventRepository : IDiscoveryEventRepository
{
    private readonly AppDbContext _context;

    public DiscoveryEventRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DiscoveryEvent>> GetPublicEventsAsync()
    {
        return await _context.Set<DiscoveryEvent>()
            .Where(e => e.IsPublic && e.Status == DiscoveryEventStatus.Active)
            .OrderBy(e => e.StartDate)
            .ToListAsync();
    }

    public async Task<DiscoveryEvent?> GetByIdAsync(int id)
    {
        return await _context.Set<DiscoveryEvent>()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(DiscoveryEvent ev)
    {
        await _context.Set<DiscoveryEvent>().AddAsync(ev);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Set<DiscoveryEvent>()
            .AnyAsync(e => e.Id == id);
    }
}