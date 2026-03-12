using Airlines.API.Contracts.Responses;
using Airlines.API.Contracts.Tickets;
using Airlines.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AirlinesDbContext _dbContext;

    public TicketRepository(AirlinesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResult<TicketSearchResultDto>> SearchTicketsAsync(
        TicketSearchRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Tickets
            .AsNoTracking()
            .Include(t => t.Passenger)
            .Include(t => t.Flight)
            .AsQueryable();

        if (request.TicketId is not null)
        {
            query = query.Where(t => t.TicketId == request.TicketId.Value);
        }

        if (request.ClientId is not null)
        {
            query = query.Where(t => t.ClientId == request.ClientId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.FirstName))
        {
            var firstName = request.FirstName.Trim().ToUpperInvariant();
            query = query.Where(t => t.Passenger != null &&
                                     t.Passenger.FirstName.ToUpper() == firstName);
        }

        if (!string.IsNullOrWhiteSpace(request.LastName))
        {
            var lastName = request.LastName.Trim().ToUpperInvariant();
            query = query.Where(t => t.Passenger != null &&
                                     t.Passenger.LastName.ToUpper() == lastName);
        }

        if (!string.IsNullOrWhiteSpace(request.FlightNumber))
        {
            var flightNumber = request.FlightNumber.Trim();
            query = query.Where(t => t.Flight != null &&
                                     t.Flight.FlightNum == flightNumber);
        }

        if (request.FlightDate is not null)
        {
            var date = request.FlightDate.Value;
            query = query.Where(t => t.Flight != null &&
                                     t.Flight.FlightDate == date);
        }

        query = query
            .OrderBy(t => t.TicketId)
            .ThenBy(t => t.Flight!.FlightDate)
            .ThenBy(t => t.Flight!.DepTime);

        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 20 : request.PageSize;

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TicketSearchResultDto
            {
                TicketId = t.TicketId,
                ClientId = t.ClientId,
                PassengerFirstName = t.Passenger != null ? t.Passenger.FirstName : string.Empty,
                PassengerLastName = t.Passenger != null ? t.Passenger.LastName : string.Empty,
                FlightNumber = t.Flight != null ? t.Flight.FlightNum : string.Empty,
                FlightDate = t.Flight != null ? t.Flight.FlightDate : default,
                DepartureTime = t.Flight != null ? t.Flight.DepTime : default,
                ArrivalTime = t.Flight != null ? t.Flight.ArrTime : default,
                DepartureAirportId = t.Flight != null ? t.Flight.AirportDepId : 0,
                ArrivalAirportId = t.Flight != null ? t.Flight.AirportArrId : 0,
                Seat = t.Seat
            })
            .ToListAsync(cancellationToken);

        return new PagedResult<TicketSearchResultDto>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}

