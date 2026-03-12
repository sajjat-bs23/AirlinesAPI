namespace Airlines.API.Contracts.Flights;

public class FlightSearchRequest
{
    public string? FlightNumber { get; init; }
    public DateOnly? FlightDate { get; init; }
    public int? DepartureAirportId { get; init; }
    public int? ArrivalAirportId { get; init; }

    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 50;
}

