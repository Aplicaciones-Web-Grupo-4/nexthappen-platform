using nexthappen_backend.CreateEvent.Application.Services;
using nexthappen_backend.CreateEvent.Domain.Entities;
using nexthappen_backend.CreateEvent.Domain.ValueObjects;

namespace nexthappen_backend.CreateEvent.Application.UseCases;

public class CreateEventHandler
{
    private readonly CreateEventService _service;

    public CreateEventHandler(CreateEventService service)
    {
        _service = service;
    }

    public async Task<Event> HandleAsync(dynamic request)
    {
        var dateRange = new EventDateRange(
            (DateTime)request.StartDate,
            (DateTime)request.EndDate
        );
        
        return await _service.ExecuteAsync(
            request.Organizer,
            request.Title,
            request.Description,
            request.Price,
            request.Quantity,
            request.Category,
            request.Address,
            request.Location,
            request.Photos,
            dateRange
        );
    }
}