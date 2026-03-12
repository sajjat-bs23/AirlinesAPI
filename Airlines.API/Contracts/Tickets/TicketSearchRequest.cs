namespace Airlines.API.Contracts.Tickets;

public class TicketSearchRequest
{
    public int? TicketId { get; init; }
    public int? ClientId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? FlightNumber { get; init; }
    public DateOnly? FlightDate { get; init; }

    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 20;
}

