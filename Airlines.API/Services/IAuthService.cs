using Airlines.API.Contracts.Auth;

namespace Airlines.API.Services;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<AuthResponse?> RefreshAsync(string refreshToken, CancellationToken cancellationToken = default);
}

