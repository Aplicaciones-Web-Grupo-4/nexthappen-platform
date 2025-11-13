using Microsoft.AspNetCore.Mvc;
using nexthappen_backend.CreateEvent.Application.Services;
using nexthappen_backend.CreateEvent.Domain.Entities;
using nexthappen_backend.CreateEvent.Domain.ValueObjects;
using System.Text.Json;

namespace nexthappen_backend.CreateEvent.API.Controllers;

[ApiController]
[Route("api/events")]
public class EventController : ControllerBase
{
    private readonly CreateEventService _service;

    public EventController(CreateEventService service)
    {
        _service = service;
    }

    // ✅ POST /api/events
    [HttpPost]
public async Task<IActionResult> Create([FromBody] JsonElement body)
{
    try
    {
        // ⚙️ Helper local para leer strings sin error
        string GetStringOrEmpty(string propName)
        {
            return body.TryGetProperty(propName, out var prop) && 
                   prop.ValueKind == JsonValueKind.String
                   ? prop.GetString() ?? string.Empty
                   : string.Empty;
        }

        // ✅ Leer propiedades
        string organizer = GetStringOrEmpty("organizer");
        string title = GetStringOrEmpty("title");
        string description = GetStringOrEmpty("description");
        string category = GetStringOrEmpty("category");
        string address = GetStringOrEmpty("address");
        string location = GetStringOrEmpty("location");

        decimal? price = body.TryGetProperty("price", out var p) && p.ValueKind == JsonValueKind.Number
            ? p.GetDecimal()
            : (decimal?)null;

        int? quantity = body.TryGetProperty("quantity", out var q) && q.ValueKind == JsonValueKind.Number
            ? q.GetInt32()
            : (int?)null;

        // ✅ Fotos
        var photos = new List<string>();
        if (body.TryGetProperty("photos", out var photosProp) && photosProp.ValueKind == JsonValueKind.Array)
        {
            foreach (var photo in photosProp.EnumerateArray())
            {
                if (photo.ValueKind == JsonValueKind.String)
                    photos.Add(photo.GetString()!);
            }
        }

        // ✅ Fechas
        DateTime startDate = body.TryGetProperty("startDate", out var s) && s.ValueKind == JsonValueKind.String
            ? DateTime.Parse(s.GetString()!)
            : DateTime.UtcNow;

        DateTime endDate = body.TryGetProperty("endDate", out var e) && e.ValueKind == JsonValueKind.String
            ? DateTime.Parse(e.GetString()!)
            : DateTime.UtcNow.AddDays(1);

        var dateRange = new EventDateRange(startDate, endDate);

        // ✅ Crear evento
        var ev = await _service.ExecuteAsync(
            organizer,
            title,
            description,
            price,
            quantity,
            category,
            address,
            location,
            photos,
            dateRange
        );

        return CreatedAtAction(nameof(GetById), new { id = ev.Id }, ev);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[❌ ERROR CREATE EVENT]: {ex.Message}");
        return BadRequest(new { message = ex.Message });
    }
}


    // ✅ GET /api/events/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromServices] IEventRepository repo, Guid id)
    {
        var ev = await repo.GetByIdAsync(id);
        return ev is null ? NotFound() : Ok(ev);
    }

    // ✅ GET /api/events
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] IEventRepository repo)
    {
        var events = await repo.GetAllAsync();
        return Ok(events);
    }
    
    // ✅ PUT /api/events/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] JsonElement body,
        [FromServices] IEventRepository repo)
    {
        try
        {
            var existing = await repo.GetByIdAsync(id);
            if (existing is null)
                return NotFound(new { message = "Evento no encontrado" });

            // 🔄 Actualizar campos básicos
            if (body.TryGetProperty("organizer", out var org)) existing.UpdateOrganizer(org.GetString() ?? "");
            if (body.TryGetProperty("title", out var t)) existing.UpdateTitle(t.GetString() ?? "");
            if (body.TryGetProperty("description", out var d)) existing.UpdateDescription(d.GetString() ?? "");
            if (body.TryGetProperty("price", out var p) && p.ValueKind == JsonValueKind.Number)
                existing.UpdatePrice(p.GetDecimal());
            if (body.TryGetProperty("quantity", out var q) && q.ValueKind == JsonValueKind.Number)
                existing.UpdateQuantity(q.GetInt32());
            if (body.TryGetProperty("category", out var c)) existing.UpdateCategory(c.GetString() ?? "");
            if (body.TryGetProperty("address", out var a)) existing.UpdateAddress(a.GetString() ?? "");
            if (body.TryGetProperty("location", out var l)) existing.UpdateLocation(l.GetString() ?? "");

            // 🖼️ Actualizar fotos (array)
            if (body.TryGetProperty("photos", out var photosProp) && photosProp.ValueKind == JsonValueKind.Array)
            {
                var photos = photosProp.EnumerateArray().Select(p => p.GetString() ?? "").ToList();
                existing.UpdatePhotos(photos);
            }

            // 📅 Actualizar fechas
            if (body.TryGetProperty("dateRange", out var drProp) && drProp.ValueKind == JsonValueKind.Object)
            {
                var start = drProp.GetProperty("startDate").GetDateTime();
                var end = drProp.GetProperty("endDate").GetDateTime();
                existing.UpdateDateRange(new EventDateRange(start, end));
            }

            await repo.UpdateAsync(existing);

            return Ok(existing);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[? ERROR UPDATE EVENT]: {ex.Message}");
            return BadRequest(new { message = ex.Message });
        }
    }
    
}
