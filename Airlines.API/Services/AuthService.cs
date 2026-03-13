using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Airlines.API.Contracts.Auth;
using Airlines.API.Repositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Airlines.API.Services;

public class AuthService : IAuthService
{
    private static readonly Dictionary<string, int> RefreshTokens = new();

    private readonly IEmployeeRepository _employeeRepository;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IEmployeeRepository employeeRepository, IOptions<JwtSettings> jwtOptions)
    {
        _employeeRepository = employeeRepository;
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmpId, cancellationToken);

        if (employee is null)
        {
            return null;
        }

        if (!string.Equals(employee.Password, request.Password))
        {
            return null;
        }

        var (accessToken, expiresAtUtc) = GenerateAccessToken(employee.EmpId);
        var refreshToken = GenerateRefreshToken();

        lock (RefreshTokens)
        {
            RefreshTokens[refreshToken] = employee.EmpId;
        }

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAtUtc = expiresAtUtc
        };
    }

    public Task<AuthResponse?> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default)
    {
        int empId;
        lock (RefreshTokens)
        {
            if (!RefreshTokens.TryGetValue(refreshToken, out empId))
            {
                return Task.FromResult<AuthResponse?>(null);
            }
        }

        var (accessToken, expiresAtUtc) = GenerateAccessToken(empId);
        var newRefreshToken = GenerateRefreshToken();

        lock (RefreshTokens)
        {
            RefreshTokens.Remove(refreshToken);
            RefreshTokens[newRefreshToken] = empId;
        }

        var response = new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresAtUtc = expiresAtUtc
        };

        return Task.FromResult<AuthResponse?>(response);
    }

    private (string token, DateTime expiresAtUtc) GenerateAccessToken(int empId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, empId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, empId.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return (tokenString, expires);
    }

    private static string GenerateRefreshToken()
    {
        var bytes = new byte[64];
        RandomNumberGenerator.Fill(bytes);
        return Convert.ToBase64String(bytes);
    }
}

