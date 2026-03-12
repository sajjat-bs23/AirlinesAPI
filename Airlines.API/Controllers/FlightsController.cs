using Airlines.API.Contracts.Flights;
using Airlines.API.Contracts.Responses;
using Airlines.API.Data;
using Airlines.API.Models;
using Airlines.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly IFlightSearchService _flightSearchService;
    private readonly AirlinesDbContext _context;

    public FlightsController(AirlinesDbContext context, IFlightSearchService flightSearchService)
    {
        _context = context;
        _flightSearchService = flightSearchService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
    {
        return await _context.Flights.AsNoTracking().ToListAsync();
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResponse<PagedResult<FlightSearchResultDto>>>> SearchFlights(
        [FromQuery] FlightSearchRequest request,
        CancellationToken cancellationToken)
    {
        var hasAnyCriteria =
            !string.IsNullOrWhiteSpace(request.FlightNumber) ||
            request.FlightDate is not null ||
            request.DepartureAirportId is not null ||
            request.ArrivalAirportId is not null;

        if (!hasAnyCriteria)
        {
            return Ok(ApiResponse<PagedResult<FlightSearchResultDto>>.Fail(
                "INSERT THE DATA TO DO A RESEARCH"));
        }

        var result = await _flightSearchService.SearchAsync(request, cancellationToken);

        if (result.TotalCount == 0)
        {
            return Ok(ApiResponse<PagedResult<FlightSearchResultDto>>.Fail("No flight found"));
        }

        return Ok(ApiResponse<PagedResult<FlightSearchResultDto>>.Ok(result));
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

