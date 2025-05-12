using BookService.Application.UseCases.Books.Commands;
using BookService.Application.UseCases.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Api.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly ISender _sender;

    public BooksController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateBookCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllBooksQuery query)
    {
        var result = await _sender.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error.Message);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Get([FromQuery] GetBookQuery query)
    {
        var result = await _sender.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error.Message);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] DeleteBookCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error.Message);
    }
}
