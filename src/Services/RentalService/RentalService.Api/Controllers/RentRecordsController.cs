using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalService.Application.UseCases.RentalRecords.Commands;

namespace RentalService.Api.Controllers;

[ApiController]
[Route("rents")]
public class RentRecordsController : ControllerBase
{
    private readonly ISender _sender;

    public RentRecordsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenerateRentCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Value);
    }

    [HttpPost("close")]
    public async Task<IActionResult> Close([FromQuery] CloseRentCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result);
    }
}
