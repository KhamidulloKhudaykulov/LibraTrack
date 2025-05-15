using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityService.Api.Authentication;

public class JwtService : IJwtService
{
    private const string SecretKey = "this_is_a_very_long_secret_key_with_32_chars!";
    public async Task<string> GenerateTokenAsync(string login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, login)
            }),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature),
            Expires = DateTime.UtcNow.AddDays(1)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return await Task.FromResult(tokenHandler.WriteToken(token));
    }

    public async Task<ClaimsPrincipal> VerifyTokenAsync(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);

        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);
            return await Task.FromResult(principal);
        }
        catch
        {
            return null; // token noto‘g‘ri yoki muddati o‘tgan
        }
    }
}
