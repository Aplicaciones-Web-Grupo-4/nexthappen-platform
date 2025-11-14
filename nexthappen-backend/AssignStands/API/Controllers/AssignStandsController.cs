using Microsoft.AspNetCore.Mvc;
using nexthappen_backend.AssignStands.Application.Services;
using nexthappen_backend.AssignStands.Domain.Entities;

namespace nexthappen_backend.AssignStands.API.Controllers;

[ApiController]
[Route("api/events/{eventId:guid}/stands")]
public class AssignStandsController : ControllerBase
{
    private readonly AssignStandsService _service;

    public AssignStandsController(AssignStandsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssigned(Guid eventId)
    {
        return Ok(await _service.GetAssignedAsync(eventId));
    }

    [HttpPost]
    public async Task<IActionResult> Assign(Guid eventId, [FromBody] AssignedStand body)
    {
        var result = await _service.AssignAsync(eventId, body.Name, body.Category);
        return Ok(result);
    }
}

[ApiController]
[Route("api/stands")]
public class StandEditController : ControllerBase
{
    private readonly AssignStandsService _service;

    public StandEditController(AssignStandsService service)
    {
        _service = service;
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] AssignedStand body)
    {
        var updated = await _service.UpdateAsync(id, body.Name, body.Category);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? Ok() : NotFound();
    }
}