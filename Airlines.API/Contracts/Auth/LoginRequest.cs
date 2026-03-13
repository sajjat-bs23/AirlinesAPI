namespace Airlines.API.Contracts.Auth;

public class LoginRequest
{
    public int EmpId { get; set; }
    public string Password { get; set; } = string.Empty;
}

