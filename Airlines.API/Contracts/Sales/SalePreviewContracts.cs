namespace Airlines.API.Contracts.Sales;

public class SalePreviewRequest
{
    // CLIIDI (string to allow COBOL-style numeric validation)
    public string ClientId { get; set; } = string.Empty;

    // FNUMI
    public string FlightNumber { get; set; } = string.Empty;

    // FDATEI, expected format YYYY-MM-DD
    public string FlightDate { get; set; } = string.Empty;

    // PASSNI
    public string PassengerCount { get; set; } = string.Empty;

    // From COMMAREA in COBOL – optional preselected flight
    public int? PreselectedFlightId { get; set; }

    // Current user id (salesperson)
    public int UserId { get; set; }
}

public class SalePreviewResponse
{
    public bool IsValid { get; set; }
    public string Message { get; set; } = string.Empty;

    public int? ClientId { get; set; }
    public string? PassengerFirstName { get; set; }
    public string? PassengerLastName { get; set; }

    public int? FlightId { get; set; }
    public string? FlightNumber { get; set; }
    public DateOnly? FlightDate { get; set; }
    public TimeOnly? DepTime { get; set; }
    public TimeOnly? ArrTime { get; set; }
    public string? AirportDeparture { get; set; }
    public string? AirportArrival { get; set; }

    public int PassengerCount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    // Equivalent of WS-FLAG-MOUV
    public bool CanProceedToStep2 { get; set; }
}

