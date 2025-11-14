using nexthappen_backend.Tickets.Application.Services;

namespace nexthappen_backend.Tickets.Application.UseCases;

public class PurchaseTicketHandler
{
    private readonly TicketsService _service;

    public PurchaseTicketHandler(TicketsService service)
    {
        _service = service;
    }

    public Task<int> Handle(int userId, int eventId)
        => _service.PurchaseTicketAsync(userId, eventId);
}