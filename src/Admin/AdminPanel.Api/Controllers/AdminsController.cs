using AdminPanel.Api.Application.Commands;
using AdminPanel.Api.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Api.Controllers;

[ApiController]
[Route("api/admins")]
public class AdminsController : ControllerBase
{
    private readonly CreateAdminRequestHandler _createAdminRequestHandler;
    private readonly GetVerifiedAdminRequestHandler _getVerifiedAdminRequestHandler;

    public AdminsController(CreateAdminRequestHandler createAdminRequestHandler, GetVerifiedAdminRequestHandler getVerifiedAdminRequestHandler)
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

    [HttpGet]
    public async Task<IActionResult> Verify([FromQuery]GetVerifiedAdminRequest request)
    {
        try
        {
            var result = await _getVerifiedAdminRequestHandler.Handle(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }
}
