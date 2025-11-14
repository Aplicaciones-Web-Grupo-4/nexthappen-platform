using nexthappen_backend.AssignStands.Domain.Entities;
using nexthappen_backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace nexthappen_backend.AssignStands.Infrastructure.Persistence.Repositories;

public class AssignedStandRepository : IAssignedStandRepository
{
    private readonly AppDbContext _context;

    public AssignedStandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AssignedStand stand)
    {
        await _context.AssignedStands.AddAsync(stand);
        await _context.SaveChangesAsync();
    }

    public async Task<List<AssignedStand>> GetByEventIdAsync(Guid eventId)
    {
        return await _context.AssignedStands
            .Where(x => x.EventId == eventId)
            .ToListAsync();
    }

    public async Task<AssignedStand?> GetByIdAsync(Guid id)
    {
        return await _context.AssignedStands.FindAsync(id);
    }

    public async Task UpdateAsync(AssignedStand stand)
    {
        _context.AssignedStands.Update(stand);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var stand = await _context.AssignedStands.FindAsync(id);
        if (stand != null)
        {
            _context.AssignedStands.Remove(stand);
            await _context.SaveChangesAsync();
        }
    }
}