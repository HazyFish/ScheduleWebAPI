using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ScheduleWebAPI;

[Route("events")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly EventDb db;

    public EventsController(EventDb db) => this.db = db;

    /// <summary>
    /// List events
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Event>>> ListAsync(bool includesOfficeHours = false)
        => includesOfficeHours is true
        ? await db.Events.ToListAsync()
        : await db.Events.Where(e => e.Type != "Office Hour").ToListAsync();

    /// <summary>
    /// Get an event by id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(void))]
    public async Task<ActionResult<Event>> GetAsync(int id)
        => await db.Events.FindAsync(id) is Event todo ? todo : NotFound();

    /// <summary>
    /// Create an event
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> CreateAsync(Event e)
    {
        db.Events.Add(e);
        await db.SaveChangesAsync();
        return Created($"/events/{e.Id}", null);
    }

    /// <summary>
    /// Delete an event
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesErrorResponseType(typeof(void))]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        if (await db.Events.FindAsync(id) is Event e)
        {
            db.Events.Remove(e);
            await db.SaveChangesAsync();
            return NoContent();
        }

        return NotFound();
    }
}
