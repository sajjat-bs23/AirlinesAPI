namespace Airlines.API.Models;

public class Department
{
    public int DeptId { get; set; }
    public string Name { get; set; } = string.Empty;

    public int? ManagerId { get; set; }

    public Employee? Manager { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

