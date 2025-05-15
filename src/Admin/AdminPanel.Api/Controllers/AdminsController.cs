using AdminPanel.Api.Application.Commands;
using AdminPanel.Api.Application.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace AdminPanel.Api.Controllers;

[ApiController]
[Route("api/admins")]
public class AdminsController : ControllerBase
{
    private readonly CreateAdminRequestHandler _createAdminRequestHandler;
    private readonly GetVerifiedAdminRequestHandler _getVerifiedAdminRequestHandler;
    private readonly HttpClient _httpClient;

    public AdminsController(
        CreateAdminRequestHandler createAdminRequestHandler,
        GetVerifiedAdminRequestHandler getVerifiedAdminRequestHandler,
        IHttpClientFactory httpClientFactory)
    {
        _createAdminRequestHandler = createAdminRequestHandler;
        _getVerifiedAdminRequestHandler = getVerifiedAdminRequestHandler;
        _httpClient = httpClientFactory.CreateClient("IdentityService");
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
            var result = await _getVerifiedAdminRequestHandler.Handle(request);
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", result);

            Response.Cookies.Append("access-token", result, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddSeconds(3)
            });

            return Ok("Token verified");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }
}
