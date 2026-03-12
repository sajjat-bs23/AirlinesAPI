namespace Airlines.API.Models;

public class Airplane
{
    public int AirplaneId { get; set; }
    public string Type { get; set; } = string.Empty;
    public int NumSeats { get; set; }
    public decimal TotalFuel { get; set; }

    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}

