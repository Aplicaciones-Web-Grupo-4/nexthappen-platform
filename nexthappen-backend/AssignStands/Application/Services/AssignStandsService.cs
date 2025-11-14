using nexthappen_backend.AssignStands.Domain.Entities;

namespace nexthappen_backend.AssignStands.Application.Services;

public class AssignStandsService
{
    private readonly IAssignedStandRepository _repo;

    public AssignStandsService(IAssignedStandRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<AssignedStand>> GetAssignedAsync(Guid eventId)
    {
        return await _repo.GetByEventIdAsync(eventId);
    }

    public async Task<AssignedStand> AssignAsync(Guid eventId, string name, string category)
    {
        var stand = new AssignedStand(eventId, name, category);
        await _repo.AddAsync(stand);
        return stand;
    }

    public async Task<AssignedStand?> UpdateAsync(Guid id, string name, string category)
    {
        var stand = await _repo.GetByIdAsync(id);
        if (stand == null) return null;

        stand.Update(name, category);
        await _repo.UpdateAsync(stand);
        return stand;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var stand = await _repo.GetByIdAsync(id);
        if (stand == null) return false;

        await _repo.DeleteAsync(id);
        return true;
    }
}