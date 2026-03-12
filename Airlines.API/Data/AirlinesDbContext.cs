using Airlines.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Data;

public class AirlinesDbContext : DbContext
{
    public AirlinesDbContext(DbContextOptions<AirlinesDbContext> options) : base(options)
    {
    }

    public DbSet<Flight> Flights => Set<Flight>();
}

