namespace Airlines.API.Contracts.Sales;

public class ConfirmSaleRequest
{
    public int ClientId { get; set; }
    public int FlightId { get; set; }
    public string FlightNumber { get; set; } = string.Empty;
    public DateOnly FlightDate { get; set; }

    public int PassengerCount { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    // Current user id (salesperson)
    public int EmpId { get; set; }
}

public class ConfirmSaleResponse
{
    public bool IsAccepted { get; set; }
    public string Message { get; set; } = string.Empty;
    public int? BuyId { get; set; }
}

