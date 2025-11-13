using nexthappen_backend.ManageEvent.Application.Services;

namespace nexthappen_backend.ManageEvent.Application.UseCases;

public class DeleteEventHandler
{
    private readonly ManageEventService _service;

    public DeleteEventHandler(ManageEventService service)
    {
        _service = service;
    }

    public async Task<bool> HandleAsync(Guid id)
    {
        return await _service.DeleteEventAsync(id);
    }
}