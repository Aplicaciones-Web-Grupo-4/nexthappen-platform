using nexthappen_backend.Tickets.Application.Services;
using nexthappen_backend.Tickets.Domain.Entities;

namespace nexthappen_backend.Tickets.Application.UseCases;

public class GetUserTicketsHandler
{
    private readonly TicketsService _service;

    public GetUserTicketsHandler(TicketsService service)
    {
        _service = service;
    }

    public Task<IEnumerable<Ticket>> Handle(int userId)
        => _service.GetUserTicketsAsync(userId);
}