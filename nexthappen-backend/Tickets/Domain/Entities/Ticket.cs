using nexthappen_backend.Tickets.Domain.ValueObjects;

namespace nexthappen_backend.Tickets.Domain.Entities;

public class Ticket
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public int EventId { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public TicketStatus Status { get; private set; }

    protected Ticket() { }

    public Ticket(int userId, int eventId)
    {
        UserId = userId;
        EventId = eventId;
        PurchaseDate = DateTime.UtcNow;
        Status = TicketStatus.Active;
    }

    public void Cancel()
    {
        Status = TicketStatus.Cancelled;
    }
}