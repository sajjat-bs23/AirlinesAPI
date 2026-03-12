namespace Airlines.API.Models;

public class Employee
{
    public int EmpId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Addre { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Telephone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly AdmiDate { get; set; }
    public decimal Salary { get; set; }

    public int DeptId { get; set; }

    public Department? Department { get; set; }
    public ICollection<Buy> Sales { get; set; } = new List<Buy>();
    public ICollection<Crew> CrewsAsCommander { get; set; } = new List<Crew>();
    public ICollection<Crew> CrewsAsCopilote { get; set; } = new List<Crew>();
    public ICollection<Crew> CrewsAsFachief { get; set; } = new List<Crew>();
    public ICollection<Crew> CrewsAsFliAttendant1 { get; set; } = new List<Crew>();
    public ICollection<Crew> CrewsAsFliAttendant2 { get; set; } = new List<Crew>();
    public ICollection<Crew> CrewsAsFliAttendant3 { get; set; } = new List<Crew>();
    public ICollection<Department> ManagedDepartments { get; set; } = new List<Department>();
}

