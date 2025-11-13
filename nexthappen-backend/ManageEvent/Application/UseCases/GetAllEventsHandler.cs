using nexthappen_backend.CreateEvent.Domain.Entities;
using nexthappen_backend.ManageEvent.Application.Services;

namespace nexthappen_backend.ManageEvent.Application.UseCases;

public class GetAllEventsHandler
{
    private readonly ManageEventService _service;

    public GetAllEventsHandler(ManageEventService service)
    {
        _service = service;
    }

    public async Task<List<Event>> HandleAsync()
    {
        return await _service.GetAllEventsAsync();
    }
}