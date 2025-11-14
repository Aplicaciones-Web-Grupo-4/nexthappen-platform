using nexthappen_backend.EventDiscovery.Domain.Entities;

namespace nexthappen_backend.EventDiscovery.Application.Services;

public class EventDiscoveryService
{
    private readonly IDiscoveryEventRepository _repository;

    public EventDiscoveryService(IDiscoveryEventRepository repository)
    {
        _repository = repository;
    }

    public Task<IEnumerable<DiscoveryEvent>> GetPublicEventsAsync()
    {
        return _repository.GetPublicEventsAsync();
    }
}