namespace Airlines.API.Models;

public class Crew
{
    public int CrewId { get; set; }

    public int CommanderId { get; set; }
    public int CopiloteId { get; set; }
    public int FachiefId { get; set; }
    public int FliAttendant1Id { get; set; }
    public int FliAttendant2Id { get; set; }
    public int FliAttendant3Id { get; set; }

    public Employee? Commander { get; set; }
    public Employee? Copilote { get; set; }
    public Employee? Fachief { get; set; }
    public Employee? FliAttendant1 { get; set; }
    public Employee? FliAttendant2 { get; set; }
    public Employee? FliAttendant3 { get; set; }

    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}

