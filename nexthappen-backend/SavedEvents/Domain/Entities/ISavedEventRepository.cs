using nexthappen_backend.SavedEvents.Domain.ValueObjects;

namespace nexthappen_backend.SavedEvents.Domain.Entities;

public interface ISavedEventRepository
{
    Task AddAsync(SavedEvent savedEvent);
    Task RemoveAsync(int userId, int eventId);
    Task<IEnumerable<SavedEvent>> GetSavedEventsAsync(int userId);
    Task<bool> ExistsAsync(int userId, int eventId);
}