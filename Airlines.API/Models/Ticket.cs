namespace Airlines.API.Models;

public class Ticket
{
    public int TicketId { get; set; }
    public int BuyId { get; set; }
    public int ClientId { get; set; }
    public int FlightId { get; set; }
    public string Seat { get; set; } = string.Empty;

    public Buy? Buy { get; set; }
    public Passenger? Passenger { get; set; }
    public Flight? Flight { get; set; }
}

