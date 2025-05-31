using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalService.Application.UseCases.RentalRecords.Commands;
using RentalService.Application.UseCases.RentalRecords.Queries;

namespace RentalService.Api.Controllers;

[ApiController]
[Route("api/rents")]
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

        return BadRequest(result.Error);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]GetAllRentsQuery query)
    {
        var result = await _sender.Send(query);
        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Error);
    }

    [HttpPost("close")]
    public async Task<IActionResult> Close([FromQuery] CloseRentCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error);
    }

    [HttpPut("pay")]
    public async Task<IActionResult> Pay([FromQuery] PayRentCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error);
    }

    [HttpPut("cancel")]
    public async Task<IActionResult> Cancel([FromQuery] CancelRentCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error);
    }
}
