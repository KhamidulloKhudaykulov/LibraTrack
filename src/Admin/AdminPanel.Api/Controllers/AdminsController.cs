using AdminPanel.Api.Application.Commands;
using AdminPanel.Api.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Api.Controllers;

[ApiController]
[Route("api/admins")]
public class AdminsController : ControllerBase
{
    private readonly CreateAdminRequestHandler _createAdminRequestHandler;
    private readonly GetVerifiedAdminRequestHandler _getVerifiedAdminRequestHandler;

    public AdminsController(
        CreateAdminRequestHandler createAdminRequestHandler,
        GetVerifiedAdminRequestHandler getVerifiedAdminRequestHandler,
        IHttpClientFactory httpClientFactory)
    {
        _createAdminRequestHandler = createAdminRequestHandler;
        _getVerifiedAdminRequestHandler = getVerifiedAdminRequestHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateAdminRequest request)
    {
        try
        {
            var result = await _createAdminRequestHandler.Handle(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }

    [HttpGet("sign-in")]
    public async Task<IActionResult> Verify([FromQuery]GetVerifiedAdminRequest request)
    {
        try
        {
            var token = await _getVerifiedAdminRequestHandler.Handle(request);

            Response.Cookies.Append("access-token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return Ok("Token verified");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        Response.Cookies.Append("access-token", "", new CookieOptions
        {
            Expires = DateTimeOffset.UtcNow.AddDays(-1), // o'tgan vaqt qilib qo'yamiz
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
        });

        await Task.CompletedTask;
        return NoContent();
    }
}
