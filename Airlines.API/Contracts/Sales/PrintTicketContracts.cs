namespace Airlines.API.Contracts.Sales;

public class PrintTicketResponse
{
    public bool IsFound { get; set; }
    public string Message { get; set; } = string.Empty;

    public int BuyId { get; set; }
    public DateOnly BuyDate { get; set; }
    public TimeOnly BuyTime { get; set; }

    public int ClientId { get; set; }
    public string PassengerFirstName { get; set; } = string.Empty;
    public string PassengerLastName { get; set; } = string.Empty;

    public string FlightNumber { get; set; } = string.Empty;
    public DateOnly FlightDate { get; set; }
    public TimeOnly DepartureTime { get; set; }
    public TimeOnly ArrivalTime { get; set; }
    public string AirportDeparture { get; set; } = string.Empty;
    public string AirportArrival { get; set; } = string.Empty;

    public int TicketCount { get; set; }
    public decimal TotalPrice { get; set; }
    public List<string> Seats { get; set; } = new();
}
