using Microsoft.AspNetCore.Mvc;
using nexthappen_backend.Tickets.Application.Services;
using nexthappen_backend.Tickets.Application.UseCases;

namespace nexthappen_backend.Tickets.API.Controllers;

[ApiController]
public class TicketsController : ControllerBase
{
    private readonly PurchaseTicketHandler _purchaseHandler;
    private readonly GetUserTicketsHandler _listHandler;
    private readonly GetTicketByIdHandler _detailHandler;
    private readonly TicketsService _service;

    public TicketsController(
        PurchaseTicketHandler purchaseHandler,
        GetUserTicketsHandler listHandler,
        GetTicketByIdHandler detailHandler,
        TicketsService service)
    {
        _purchaseHandler = purchaseHandler;
        _listHandler = listHandler;
        _detailHandler = detailHandler;
        _service = service;
    }

    [HttpPost("api/events/{eventId}/tickets/purchase")]
    public async Task<IActionResult> Purchase(int eventId, int userId = 1) 
    {
        int ticketId = await _purchaseHandler.Handle(userId, eventId);
        return Ok(new { TicketId = ticketId });
    }

    [HttpGet("api/users/{userId}/tickets")]
    public async Task<IActionResult> GetUserTickets(int userId)
    {
        var tickets = await _listHandler.Handle(userId);
        return Ok(tickets);
    }

    [HttpGet("api/tickets/{ticketId}")]
    public async Task<IActionResult> GetTicketDetail(int ticketId)
    {
        var ticket = await _detailHandler.Handle(ticketId);
        return ticket != null ? Ok(ticket) : NotFound();
    }

    [HttpDelete("api/tickets/{ticketId}")]
    public async Task<IActionResult> CancelTicket(int ticketId)
    {
        var result = await _service.CancelTicketAsync(ticketId);
        return result ? Ok() : NotFound();
    }
}