using Airlines.API.Models;

namespace Airlines.API.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(int empId, CancellationToken cancellationToken = default);
}

