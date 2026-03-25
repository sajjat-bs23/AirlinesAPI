using Airlines.API.Contracts.Passengers;
using Airlines.API.Data;
using Airlines.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Repositories;

public class PassengerRepository : IPassengerRepository
{
    private readonly AirlinesDbContext _dbContext;

    public PassengerRepository(AirlinesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<PassengerKeyData>> GetPassengerKeyDataAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Passengers
            .AsNoTracking()
            .Select(p => new PassengerKeyData
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Address = p.Address,
                City = p.City,
                Country = p.Country,
                ZipCode = p.ZipCode,
                Email = p.Email
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<int> AddPassengersAsync(IReadOnlyList<Passenger> passengers, CancellationToken cancellationToken = default)
    {
        if (passengers.Count == 0)
        {
            return 0;
        }

        // Keep PostgreSQL identity sequence aligned with existing rows.
        await _dbContext.Database.ExecuteSqlRawAsync(
            """
            SELECT setval(
                pg_get_serial_sequence('"Passengers"', 'ClientId'),
                COALESCE((SELECT MAX("ClientId") FROM "Passengers"), 1),
                
                true
            );
            """,
            cancellationToken);

        await _dbContext.Passengers.AddRangeAsync(passengers, cancellationToken);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
