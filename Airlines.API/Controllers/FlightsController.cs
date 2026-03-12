using Airlines.API.Data;
using Airlines.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly AirlinesDbContext _context;

    public FlightsController(AirlinesDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
    {
        return await _context.Flights.AsNoTracking().ToListAsync();
    }

    //[HttpGet("{id:int}")]
    //public async Task<ActionResult<Flight>> GetFlight(int id)
    //{
    //    var flight = await _context.Flights.FindAsync(id);
    //    if (flight is null)
    //    {
    //        return NotFound();
    //    }

    //    return flight;
    //}

    //[HttpPost]
    //public async Task<ActionResult<Flight>> CreateFlight(Flight flight)
    //{
    //    _context.Flights.Add(flight);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction(nameof(GetFlight), new { id = flight.Id }, flight);
    //}
}

