using nexthappen_backend.SavedEvents.Domain.ValueObjects;

namespace nexthappen_backend.SavedEvents.Domain.Entities;

public class SavedEvent
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int EventId { get; private set; }
    public SavedEventStatus Status { get; private set; }

    protected SavedEvent() { }

    public SavedEvent(int userId, int eventId)
    {
        UserId = userId;
        EventId = eventId;
        Status = SavedEventStatus.Active;
    }

    public void Deactivate() => Status = SavedEventStatus.Inactive;
}