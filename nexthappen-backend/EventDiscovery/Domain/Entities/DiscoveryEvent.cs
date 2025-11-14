using nexthappen_backend.EventDiscovery.Domain.ValueObjects;
namespace nexthappen_backend.EventDiscovery.Domain.Entities;

public class DiscoveryEvent
{
    public int Id { get; set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime StartDate { get; private set; }
    public bool IsPublic { get; private set; }
    public DiscoveryEventStatus Status { get; private set; }

    protected DiscoveryEvent() {} 

    public DiscoveryEvent(string title, string description, DateTime startDate, bool isPublic)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
        IsPublic = isPublic;
        Status = DiscoveryEventStatus.Active;
    }

    public void MarkAsPublic() => IsPublic = true;
    public void MarkAsPrivate() => IsPublic = false;

    public void UpdateInfo(string title, string description, DateTime startDate)
    {
        Title = title;
        Description = description;
        StartDate = startDate;
    }
}