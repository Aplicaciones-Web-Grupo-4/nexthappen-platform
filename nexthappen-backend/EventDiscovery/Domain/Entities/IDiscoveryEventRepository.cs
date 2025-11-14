using System.Collections.Generic;
using System.Threading.Tasks;

namespace nexthappen_backend.EventDiscovery.Domain.Entities;

public interface IDiscoveryEventRepository
{
    Task<IEnumerable<DiscoveryEvent>> GetPublicEventsAsync();
    Task<DiscoveryEvent?> GetByIdAsync(int id);
    Task AddAsync(DiscoveryEvent ev);
    Task<bool> ExistsAsync(int id);
}