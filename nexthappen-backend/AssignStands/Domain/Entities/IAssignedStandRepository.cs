namespace nexthappen_backend.AssignStands.Domain.Entities;

public interface IAssignedStandRepository
{
    Task<List<AssignedStand>> GetByEventIdAsync(Guid eventId);
    Task<AssignedStand?> GetByIdAsync(Guid id);
    Task AddAsync(AssignedStand stand);
    Task UpdateAsync(AssignedStand stand);
    Task DeleteAsync(Guid id);
}