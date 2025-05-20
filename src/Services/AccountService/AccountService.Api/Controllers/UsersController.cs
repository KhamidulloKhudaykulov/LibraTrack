using AccountService.Application.UseCases.Users.Commands;
using AccountService.Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Api.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddUserCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error.Message.ToString());
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get([FromQuery] GetUserQuery query)
    {
        var result = await _sender.Send(query);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error.Message.ToString());
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersQuery query)
    {
        var result = await _sender.Send(query);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error.Message.ToString());
    }

    [HttpPatch("block")]
    public async Task<IActionResult> Block([FromQuery]AddUserToBlackListCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error.Message.ToString());
    }

    [HttpPatch("active")]
    public async Task<IActionResult> Active([FromQuery]ActiveUserCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error.Message.ToString());
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromQuery] UpdateUserCommand command)
    {
        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error.Message.ToString());
    }
}
