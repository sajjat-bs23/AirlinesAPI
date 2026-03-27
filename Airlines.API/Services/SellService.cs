using Airlines.API.Contracts.Sales;
using Airlines.API.Data;
using Airlines.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Services;

public interface ISellService
{
    Task<SalePreviewResponse> ValidateSellStep1Async(SalePreviewRequest request, CancellationToken cancellationToken);
    Task<ConfirmSaleResponse> ConfirmSaleAsync(ConfirmSaleRequest request, CancellationToken cancellationToken);
    Task<PrintTicketResponse> GetPrintTicketAsync(int buyId, CancellationToken cancellationToken);
}

public class SellService : ISellService
{
    private readonly AirlinesDbContext _dbContext;
    private const decimal UnitPrice = 120.99m;

    public SellService(AirlinesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SalePreviewResponse> ValidateSellStep1Async(
        SalePreviewRequest request,
        CancellationToken cancellationToken)
    {
        var response = new SalePreviewResponse();

        // Validation – Client Id
        if (!int.TryParse(request.ClientId, out var clientId) || clientId == 0)
        {
            response.Message = "YOU MUST INSERT A NUMBER IN THE CLIENTID";
            return response;
        }

        // Validation – Flight Number
        if (string.IsNullOrWhiteSpace(request.FlightNumber))
        {
            response.Message = "YOU MUST INSERT A CORRECT FIGHT NUMBER";
            return response;
        }

        // Validation – Date (YYYY-MM-DD)
        if (!IsValidYyyyMmDd(request.FlightDate, out var flightDate))
        {
            response.Message = "THE CORRECT DATA FORMAT IS: YYYY-MM-DD";
            return response;
        }

        // Validation – Passenger Count
        if (!int.TryParse(request.PassengerCount, out var passengerCount) || passengerCount <= 0)
        {
            response.Message = "YOU MUST INSERT A NUMBER IN THE NUMBER OF CLIENTS";
            return response;
        }

        try
        {
            // Data – Passenger
            var passenger = await _dbContext.Passengers
                .FirstOrDefaultAsync(p => p.ClientId == clientId, cancellationToken);

            if (passenger is null)
            {
                response.Message = "THIS PASSENGER DOES NOT EXIST";
                return response;
            }

            // Data – Flight
            IQueryable<Flight> flightQuery = _dbContext.Flights
                .Include(f => f.AirportDep)
                .Include(f => f.AirportArr);

            Flight? flight;

            if (request.PreselectedFlightId.HasValue)
            {
                var id = request.PreselectedFlightId.Value;
                flight = await flightQuery
                    .FirstOrDefaultAsync(f => f.FlightId == id, cancellationToken);
            }
            else
            {
                flight = await flightQuery
                    .FirstOrDefaultAsync(
                        f => f.FlightDate == flightDate && f.FlightNum == request.FlightNumber,
                        cancellationToken);
            }

            if (flight is null)
            {
                response.Message = "THIS FLIGHT DOES NOT EXIST";
                return response;
            }

            // Price calculation
            var totalPrice = UnitPrice * passengerCount;

            response.IsValid = true;
            response.CanProceedToStep2 = true;
            response.Message = string.Empty;

            response.ClientId = passenger.ClientId;
            response.PassengerFirstName = passenger.FirstName;
            response.PassengerLastName = passenger.LastName;

            response.FlightId = flight.FlightId;
            response.FlightNumber = flight.FlightNum;
            response.FlightDate = flight.FlightDate;
            response.DepTime = flight.DepTime;
            response.ArrTime = flight.ArrTime;
            response.AirportDeparture = flight.AirportDep?.AirportId.ToString();
            response.AirportArrival = flight.AirportArr?.AirportId.ToString();

            response.PassengerCount = passengerCount;
            response.UnitPrice = UnitPrice;
            response.TotalPrice = totalPrice;

            return response;
        }
        catch (DbUpdateException ex)
        {
            response.Message = $"AN ERROR OCCURRED DURING RESEARCH PROCESS: {ex.InnerException?.Message ?? ex.Message}";
            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"AN ERROR OCCURRED DURING RESEARCH PROCESS: {ex.Message}";
            return response;
        }
    }

    public async Task<ConfirmSaleResponse> ConfirmSaleAsync(
        ConfirmSaleRequest request,
        CancellationToken cancellationToken)
    {
        var response = new ConfirmSaleResponse();

        try
        {
            // Re-load flight with airplane to check capacity and consistency
            var flight = await _dbContext.Flights
                .Include(f => f.Airplane)
                .FirstOrDefaultAsync(f => f.FlightId == request.FlightId, cancellationToken);

            if (flight is null ||
                flight.FlightNum != request.FlightNumber ||
                flight.FlightDate != request.FlightDate)
            {
                response.Message = "STEP 1 IS NOT VALID ANYMORE FOR THIS FLIGHT";
                return response;
            }

            // Capacity check
            var existingTickets = await _dbContext.Tickets
                .CountAsync(t => t.FlightId == request.FlightId, cancellationToken);

            var capacity = flight.Airplane?.NumSeats ?? 0;

            if (capacity <= 0 || existingTickets + request.PassengerCount > capacity)
            {
                response.Message = "CAPACITY EXCEEDED FOR THIS FLIGHT";
                return response;
            }

            // Create Buy + Tickets in a transaction
            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            var now = DateTime.UtcNow;
            var buy = new Buy
            {
                BuyDate = DateOnly.FromDateTime(now),
                BuyTime = TimeOnly.FromDateTime(now),
                Price = request.TotalPrice,
                EmpId = request.EmpId,
                ClientId = request.ClientId
            };

            _dbContext.Buys.Add(buy);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var startSeatNumber = existingTickets + 1;
            var tickets = new List<Ticket>();

            for (var i = 0; i < request.PassengerCount; i++)
            {
                var seatLabel = (startSeatNumber + i).ToString();

                tickets.Add(new Ticket
                {
                    BuyId = buy.BuyId,
                    ClientId = request.ClientId,
                    FlightId = request.FlightId,
                    Seat = seatLabel
                });
            }

            _dbContext.Tickets.AddRange(tickets);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            response.IsAccepted = true;
            response.BuyId = buy.BuyId;
            response.Message = "SALE CONFIRMED";

            return response;
        }
        catch (DbUpdateException ex)
        {
            response.Message = $"AN ERROR OCCURRED DURING CONFIRM PROCESS: {ex.InnerException?.Message ?? ex.Message}";
            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"AN ERROR OCCURRED DURING CONFIRM PROCESS: {ex.Message}";
            return response;
        }
    }

    public async Task<PrintTicketResponse> GetPrintTicketAsync(
        int buyId,
        CancellationToken cancellationToken)
    {
        var response = new PrintTicketResponse();

        try
        {
            var buy = await _dbContext.Buys
                .AsNoTracking()
                .Include(b => b.Client)
                .Include(b => b.Tickets)
                    .ThenInclude(t => t.Flight)
                        .ThenInclude(f => f!.AirportDep)
                .Include(b => b.Tickets)
                    .ThenInclude(t => t.Flight)
                        .ThenInclude(f => f!.AirportArr)
                .FirstOrDefaultAsync(b => b.BuyId == buyId, cancellationToken);

            if (buy is null)
            {
                response.Message = "BUY ID NOT FOUND";
                return response;
            }

            var orderedTickets = buy.Tickets
                .OrderBy(t => t.TicketId)
                .ToList();

            if (orderedTickets.Count == 0)
            {
                response.Message = "NO TICKET FOUND FOR THIS BUY";
                return response;
            }

            var firstFlight = orderedTickets[0].Flight;
            if (firstFlight is null)
            {
                response.Message = "FLIGHT DATA IS MISSING FOR THIS TICKET";
                return response;
            }

            response.IsFound = true;
            response.Message = "TICKET READY";

            response.BuyId = buy.BuyId;
            response.BuyDate = buy.BuyDate;
            response.BuyTime = buy.BuyTime;

            response.ClientId = buy.ClientId;
            response.PassengerFirstName = buy.Client?.FirstName ?? string.Empty;
            response.PassengerLastName = buy.Client?.LastName ?? string.Empty;

            response.FlightNumber = firstFlight.FlightNum;
            response.FlightDate = firstFlight.FlightDate;
            response.DepartureTime = firstFlight.DepTime;
            response.ArrivalTime = firstFlight.ArrTime;
            response.AirportDeparture = firstFlight.AirportDep?.AirportId.ToString() ?? string.Empty;
            response.AirportArrival = firstFlight.AirportArr?.AirportId.ToString() ?? string.Empty;

            response.TicketCount = orderedTickets.Count;
            response.TotalPrice = buy.Price;
            response.Seats = orderedTickets.Select(t => t.Seat).ToList();

            return response;
        }
        catch (DbUpdateException ex)
        {
            response.Message = $"AN ERROR OCCURRED DURING PRINT TICKET PROCESS: {ex.InnerException?.Message ?? ex.Message}";
            return response;
        }
        catch (Exception ex)
        {
            response.Message = $"AN ERROR OCCURRED DURING PRINT TICKET PROCESS: {ex.Message}";
            return response;
        }
    }

    private static bool IsValidYyyyMmDd(string value, out DateOnly date)
    {
        date = default;

        if (string.IsNullOrWhiteSpace(value) || value.Length != 10)
        {
            return false;
        }

        if (value[4] != '-' || value[7] != '-')
        {
            return false;
        }

        var yearPart = value[..4];
        var monthPart = value.Substring(5, 2);
        var dayPart = value.Substring(8, 2);

        if (!int.TryParse(yearPart, out var year) ||
            !int.TryParse(monthPart, out var month) ||
            !int.TryParse(dayPart, out var day))
        {
            return false;
        }

        try
        {
            date = new DateOnly(year, month, day);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

