using nexthappen_backend.CreateEvent.Domain.Entities;
using nexthappen_backend.ManageEvent.Domain;

namespace nexthappen_backend.ManageEvent.Application.Services;

public class ManageEventService
{
    private readonly IManageEventRepository _repository;

    public ManageEventService(IManageEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Event>> GetAllEventsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Event?> GetEventByIdAsync(Guid id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Event?> UpdateEventAsync(Guid id, Event updatedData)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return null;

        try
        {
            existing.UpdateDetails(
                updatedData.Organizer,
                updatedData.Title,
                updatedData.Description,
                updatedData.Price,
                updatedData.Quantity,
                updatedData.Category,
                updatedData.Address,
                updatedData.Location,
                updatedData.Photos,
                updatedData.DateRange
            );

            await _repository.UpdateAsync(existing);
            return existing;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"[ERROR VALIDATION]: {ex.Message}");
            throw;
        }
    }
    
    public async Task<bool> DeleteEventAsync(Guid id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        await _repository.DeleteByIdAsync(id);
        return true;
    }


}