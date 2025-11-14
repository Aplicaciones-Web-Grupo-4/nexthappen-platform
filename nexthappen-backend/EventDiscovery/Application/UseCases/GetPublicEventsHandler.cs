using nexthappen_backend.EventDiscovery.Application.Services;

namespace nexthappen_backend.EventDiscovery.Application.Usecases;

public class GetPublicEventsHandler
{
    private readonly EventDiscoveryService _service;

    public GetPublicEventsHandler(EventDiscoveryService service)
    {
        _service = service;
    }

    public async Task<object> Handle()
    {
        var events = await _service.GetPublicEventsAsync();

        return events.Select(e => new
        {
            e.Id,
            e.Title,
            e.Description,
            e.StartDate,
            e.IsPublic
        });
    }
}