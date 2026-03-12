using Airlines.API.Contracts.Flights;
using Airlines.API.Contracts.Responses;
using Airlines.API.Data;
using Airlines.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly AirlinesDbContext _dbContext;

    public FlightRepository(AirlinesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<FlightSearchResultDto>> SearchFlightsAsync(
        FlightSearchRequest request,
        CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var date = request.FlightDate ?? today;

        IQueryable<Flight> query = _dbContext.Flights.AsNoTracking();

        // Date: from given date (or today) and forward, like WS-EDIT-DATE logic.
        query = query.Where(f => f.FlightDate >= date);

        if (!string.IsNullOrWhiteSpace(request.FlightNumber))
        {
            var flightNum = request.FlightNumber.Trim();
            query = query.Where(f => f.FlightNum == flightNum);
        }

        if (request.DepartureAirportId is not null)
        {
            query = query.Where(f => f.AirportDepId == request.DepartureAirportId.Value);
        }

        if (request.ArrivalAirportId is not null)
        {
            query = query.Where(f => f.AirportArrId == request.ArrivalAirportId.Value);
        }

        query = query
            .OrderBy(f => f.FlightDate)
            .ThenBy(f => f.DepTime)
            .ThenBy(f => f.FlightNum);

        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 50 : request.PageSize;

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(f => new FlightSearchResultDto
            {
                FlightId = f.FlightId,
                FlightNumber = f.FlightNum,
                FlightDate = f.FlightDate,
                DepartureTime = f.DepTime,
                ArrivalTime = f.ArrTime,
                DepartureAirportId = f.AirportDepId,
                ArrivalAirportId = f.AirportArrId,
                TotalPassengers = f.TotPass,
                TotalBaggage = f.TotBagga
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<FlightSearchResultDto>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}

