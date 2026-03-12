namespace Airlines.API.Models;

public class Buy
{
    public int BuyId { get; set; }
    public DateOnly BuyDate { get; set; }
    public TimeOnly BuyTime { get; set; }
    public decimal Price { get; set; }

    public int EmpId { get; set; }
    public int ClientId { get; set; }

    public Employee? Employee { get; set; }
    public Passenger? Client { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}

