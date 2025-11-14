using nexthappen_backend.Tickets.Domain.Entities;

namespace nexthappen_backend.Tickets.Application.Services;


public class TicketsService
{
    private readonly ITicketRepository _repository;

    public TicketsService(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> PurchaseTicketAsync(int userId, int eventId)
    {
        var ticket = new Ticket(userId, eventId);

        await _repository.AddAsync(ticket);
        return ticket.Id;
    }

    public Task<IEnumerable<Ticket>> GetUserTicketsAsync(int userId)
        => _repository.GetByUserIdAsync(userId);

    public Task<Ticket?> GetTicketByIdAsync(int ticketId)
        => _repository.GetByIdAsync(ticketId);

    public async Task<bool> CancelTicketAsync(int ticketId)
    {
        var ticket = await _repository.GetByIdAsync(ticketId);

        if (ticket == null) return false;

        ticket.Cancel();
        await _repository.RemoveAsync(ticket);
        return true;
    }
}