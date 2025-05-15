using IdentityService.Api.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public IdentityController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost]
    public async Task<IActionResult> Generate(string login)
    {
        try
        {
            var token = await _jwtService.GenerateTokenAsync(login);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Verify(string token)
    {
        try
        {
            var result = await _jwtService.VerifyTokenAsync(token);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }
}
