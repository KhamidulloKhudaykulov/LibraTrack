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

    [Authorize]
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

    [HttpGet]
    public async Task<IActionResult> Verify([FromQuery]GetVerifiedAdminRequest request)
    {
        try
        {
            var token = await _getVerifiedAdminRequestHandler.Handle(request);

            Response.Cookies.Append("access-token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return Ok("Token verified");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }
}
