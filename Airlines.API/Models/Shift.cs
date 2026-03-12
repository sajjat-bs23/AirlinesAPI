namespace Airlines.API.Models;

public class Shift
{
    public int ShiftId { get; set; }
    public DateOnly ShiftDate { get; set; }
    public TimeOnly BeginTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public int CrewId { get; set; }

    public Crew? Crew { get; set; }
    public ICollection<Flight> Flights { get; set; } = new List<Flight>();
}

