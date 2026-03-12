namespace Airlines.API.Models;

public class Flight
{
    public int FlightId { get; set; }          // identity
    public DateOnly FlightDate { get; set; }
    public TimeOnly DepTime { get; set; }
    public TimeOnly ArrTime { get; set; }
    public int TotPass { get; set; }
    public int TotBagga { get; set; }
    public string FlightNum { get; set; } = string.Empty;

    public int ShiftId { get; set; }
    public int AirplaneId { get; set; }
    public int AirportDepId { get; set; }
    public int AirportArrId { get; set; }

    public Shift? Shift { get; set; }
    public Airplane? Airplane { get; set; }
    public Airport? AirportDep { get; set; }
    public Airport? AirportArr { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

