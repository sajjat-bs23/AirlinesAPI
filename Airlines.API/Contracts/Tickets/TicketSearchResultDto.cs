namespace Airlines.API.Contracts.Tickets;

public class TicketSearchResultDto
{
    public int TicketId { get; init; }
    public int ClientId { get; init; }
    public string PassengerFirstName { get; init; } = string.Empty;
    public string PassengerLastName { get; init; } = string.Empty;

    public string FlightNumber { get; init; } = string.Empty;
    public DateOnly FlightDate { get; init; }
    public TimeOnly DepartureTime { get; init; }
    public TimeOnly ArrivalTime { get; init; }

    public int DepartureAirportId { get; init; }
    public int ArrivalAirportId { get; init; }

    public string Seat { get; init; } = string.Empty;
}

