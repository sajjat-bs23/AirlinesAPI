namespace Airlines.API.Contracts.Passengers;

/// <summary>
/// Minimal passenger data used to build duplicate keys (no Telephone).
/// </summary>
public class PassengerKeyData
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
