using InventoryService.Application.UseCases.Items.Commands;
using InventoryService.Application.UseCases.Items.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Api.Contollers;

[ApiController]
[Route("api/inventory")]
public class InventoryControllers : ControllerBase
{
    private readonly ISender _sender;

    public InventoryControllers(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> ReceiveItem([FromBody]ReceiveStockCommand command)
    {
        var response = await _sender.Send(command);
        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok(response);
    }

    [HttpPut("deduct")]
    public async Task<IActionResult> DeductItem([FromBody]DeductItemFromStockCommand command)
    {
        var response = await _sender.Send(command);
        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok(response);
    }

    [HttpPut("add")]
    public async Task<IActionResult> ReceiveItem([FromBody] AddQuantityCommand command)
    {
        var response = await _sender.Send(command);
        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok(response);
    }

    [HttpGet("quantity")]
    public async Task<IActionResult> GetQuantity([FromQuery]GetAvailableItemQuantityQuery query)
    {
        var response = await _sender.Send(query);
        if (response.IsFailure)
        {
            return BadRequest(response.Error);
        }

        return Ok(response);
    }
}
