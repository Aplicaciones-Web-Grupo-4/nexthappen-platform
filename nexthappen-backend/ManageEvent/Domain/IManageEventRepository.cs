using nexthappen_backend.CreateEvent.Domain.Entities;

namespace nexthappen_backend.ManageEvent.Domain;

public interface IManageEventRepository
{
    Task<List<Event>> GetAllAsync();
    Task<Event?> GetByIdAsync(Guid id);
    Task UpdateAsync(Event updatedEvent);
    Task DeleteByIdAsync(Guid id);
}