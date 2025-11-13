using nexthappen_backend.CreateEvent.Domain.Entities;
using nexthappen_backend.ManageEvent.Application.Services;

namespace nexthappen_backend.ManageEvent.Application.UseCases;

public class UpdateEventHandler
{
    private readonly ManageEventService _service;

    public UpdateEventHandler(ManageEventService service)
    {
        _service = service;
    }

    public async Task<Event?> HandleAsync(Guid id, Event updatedData)
    {
        return await _service.UpdateEventAsync(id, updatedData);
    }
}