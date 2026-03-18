using Airlines.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Airlines.API.Data;

public class AirlinesDbContext : DbContext
{
    public AirlinesDbContext(DbContextOptions<AirlinesDbContext> options) : base(options)
    {
    }

    public DbSet<Airport> Airports => Set<Airport>();
    public DbSet<Airplane> Airplanes => Set<Airplane>();
    public DbSet<Flight> Flights => Set<Flight>();
    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Buy> Buys => Set<Buy>();
    public DbSet<Crew> Crews => Set<Crew>();
    public DbSet<Shift> Shifts => Set<Shift>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Explicit primary keys (for clarity)
        modelBuilder.Entity<Airport>().HasKey(a => a.AirportId);
        modelBuilder.Entity<Airplane>().HasKey(a => a.AirplaneId);
        modelBuilder.Entity<Flight>().HasKey(f => f.FlightId);
        modelBuilder.Entity<Passenger>().HasKey(p => p.ClientId);
        modelBuilder.Entity<Passenger>()
            .Property(p => p.ClientId)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Ticket>().HasKey(t => t.TicketId);
        modelBuilder.Entity<Employee>().HasKey(e => e.EmpId);
        modelBuilder.Entity<Department>().HasKey(d => d.DeptId);
        modelBuilder.Entity<Buy>().HasKey(b => b.BuyId);
        modelBuilder.Entity<Crew>().HasKey(c => c.CrewId);
        modelBuilder.Entity<Shift>().HasKey(s => s.ShiftId);

        // Airport ↔ Flight (departure)
        modelBuilder.Entity<Flight>()
            .HasOne(f => f.AirportDep)
            .WithMany(a => a.DepartingFlights)
            .HasForeignKey(f => f.AirportDepId)
            .OnDelete(DeleteBehavior.Restrict);

        // Airport ↔ Flight (arrival)
        modelBuilder.Entity<Flight>()
            .HasOne(f => f.AirportArr)
            .WithMany(a => a.ArrivingFlights)
            .HasForeignKey(f => f.AirportArrId)
            .OnDelete(DeleteBehavior.Restrict);

        // Flight ↔ Airplane
        modelBuilder.Entity<Flight>()
            .HasOne(f => f.Airplane)
            .WithMany(a => a.Flights)
            .HasForeignKey(f => f.AirplaneId);

        // Flight ↔ Shift
        modelBuilder.Entity<Flight>()
            .HasOne(f => f.Shift)
            .WithMany(s => s.Flights)
            .HasForeignKey(f => f.ShiftId);

        // Ticket ↔ Flight
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Flight)
            .WithMany(f => f.Tickets)
            .HasForeignKey(t => t.FlightId);

        // Ticket ↔ Passenger
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Passenger)
            .WithMany(p => p.Tickets)
            .HasForeignKey(t => t.ClientId);

        // Ticket ↔ Buy
        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Buy)
            .WithMany(b => b.Tickets)
            .HasForeignKey(t => t.BuyId);

        // Buy ↔ Employee
        modelBuilder.Entity<Buy>()
            .HasOne(b => b.Employee)
            .WithMany(e => e.Sales)
            .HasForeignKey(b => b.EmpId);

        // Buy ↔ Passenger
        modelBuilder.Entity<Buy>()
            .HasOne(b => b.Client)
            .WithMany(p => p.Buys)
            .HasForeignKey(b => b.ClientId);

        // Employee ↔ Department (membership)
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Employees)
            .HasForeignKey(e => e.DeptId);

        // Department ↔ Employee (manager)
        modelBuilder.Entity<Department>()
            .HasOne(d => d.Manager)
            .WithMany(e => e.ManagedDepartments)
            .HasForeignKey(d => d.ManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // Crew ↔ Employee roles
        modelBuilder.Entity<Crew>()
            .HasOne(c => c.Commander)
            .WithMany(e => e.CrewsAsCommander)
            .HasForeignKey(c => c.CommanderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Crew>()
            .HasOne(c => c.Copilote)
            .WithMany(e => e.CrewsAsCopilote)
            .HasForeignKey(c => c.CopiloteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Crew>()
            .HasOne(c => c.Fachief)
            .WithMany(e => e.CrewsAsFachief)
            .HasForeignKey(c => c.FachiefId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Crew>()
            .HasOne(c => c.FliAttendant1)
            .WithMany(e => e.CrewsAsFliAttendant1)
            .HasForeignKey(c => c.FliAttendant1Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Crew>()
            .HasOne(c => c.FliAttendant2)
            .WithMany(e => e.CrewsAsFliAttendant2)
            .HasForeignKey(c => c.FliAttendant2Id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Crew>()
            .HasOne(c => c.FliAttendant3)
            .WithMany(e => e.CrewsAsFliAttendant3)
            .HasForeignKey(c => c.FliAttendant3Id)
            .OnDelete(DeleteBehavior.Restrict);

        // Shift ↔ Crew
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Crew)
            .WithMany(c => c.Shifts)
            .HasForeignKey(s => s.CrewId);
    }
}

