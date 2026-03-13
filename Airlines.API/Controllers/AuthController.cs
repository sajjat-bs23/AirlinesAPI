using Airlines.API.Contracts.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Airlines.API.Services;

namespace Airlines.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);
        if (result is null)
        {
            return Unauthorized("Invalid credentials");
        }

        return Ok(result);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public ActionResult<AuthResponse> Refresh([FromBody] string refreshToken)
    {
        var result = _authService.RefreshAsync(refreshToken);
        if (result.Result is null)
        {
            return Unauthorized("Invalid refresh token");
        }

        return Ok(result.Result);
    }
}

