using nexthappen_backend.Tickets.Application.Services;
using nexthappen_backend.Tickets.Domain.Entities;

namespace nexthappen_backend.Tickets.Application.UseCases;

public class GetTicketByIdHandler
{
    private readonly TicketsService _service;

    public GetTicketByIdHandler(TicketsService service)
    {
        _service = service;
    }

    public Task<Ticket?> Handle(int ticketId)
        => _service.GetTicketByIdAsync(ticketId);
}