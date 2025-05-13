using AdminPanel.Api.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Api.Controllers;

[ApiController]
[Route("admins")]
public class AdminsController : ControllerBase
{
    private readonly CreateAdminRequestHandler _createAdminRequestHandler;

    public AdminsController(CreateAdminRequestHandler createAdminRequestHandler)
    {
        _createAdminRequestHandler = createAdminRequestHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateAdminRequest request)
    {
        try
        {
            await _createAdminRequestHandler.Create(request);
            return Ok(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }
}
