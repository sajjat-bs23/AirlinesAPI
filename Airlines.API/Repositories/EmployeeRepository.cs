using Airlines.API.Data;
using Airlines.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AirlinesDbContext _dbContext;

    public EmployeeRepository(AirlinesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Employee?> GetByIdAsync(int empId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Employees
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.EmpId == empId, cancellationToken);
    }
}

