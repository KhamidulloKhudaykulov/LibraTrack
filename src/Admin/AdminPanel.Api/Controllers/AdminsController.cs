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
            var result = await _createAdminRequestHandler.Handle(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }
}
