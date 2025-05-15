using System.Security.Claims;

namespace IdentityService.Api.Authentication;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(string login);
    Task<ClaimsPrincipal> VerifyTokenAsync(string token);
}
