namespace Airlines.API.Contracts.Flights;

public class FlightSearchResultDto
{
    public int FlightId { get; init; }
    public string FlightNumber { get; init; } = string.Empty;
    public DateOnly FlightDate { get; init; }
    public TimeOnly DepartureTime { get; init; }
    public TimeOnly ArrivalTime { get; init; }

    public int DepartureAirportId { get; init; }
    public int ArrivalAirportId { get; init; }

    public int TotalPassengers { get; init; }
    public int TotalBaggage { get; init; }
}

