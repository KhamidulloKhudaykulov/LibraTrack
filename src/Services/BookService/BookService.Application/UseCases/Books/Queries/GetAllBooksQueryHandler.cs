using BookService.Application.Abstractions.Messaging;
using BookService.Application.UseCases.Books.Contracts;
using BookService.Domain.Repositories;
using BookService.Domain.Shared;
using BookService.Domain.ValueObjects.Books;

namespace BookService.Application.UseCases.Books.Queries;

public record GetAllBooksQuery(
    string title = "",
    string author = "",
    string publisher = "",
    decimal price = 0) : IQuery<List<BookResponse>>;

public class GetAllBooksQueryHandler(
    IBookRepository _bookRepository)
    : IQueryHandler<GetAllBooksQuery, List<BookResponse>>
{
    public async Task<Result<List<BookResponse>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        var query = await _bookRepository.SelectAllAsQueryable();

        if (!string.IsNullOrWhiteSpace(request.title))
        {
            var titleResult = Title.Create(request.title);
            if (titleResult.IsFailure) return Result.Failure<List<BookResponse>>(titleResult.Error);
            query = query.Where(b => b.Title.Value.ToLower().Contains(titleResult.Value.Value.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(request.author))
        {
            var authorResult = Author.Create(request.author);
            if (authorResult.IsFailure) return Result.Failure<List<BookResponse>>(authorResult.Error);
            query = query.Where(b => b.Author.Value.ToLower().Contains(authorResult.Value.Value.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(request.publisher))
        {
            var publisherResult = Publisher.Create(request.publisher);
            if (publisherResult.IsFailure) return Result.Failure<List<BookResponse>>(publisherResult.Error);
            query = query.Where(b => b.Publisher.Value.ToLower().Contains(publisherResult.Value.Value.ToLower()));
        }

        if (request.price > 0)
        {
            var priceResult = Price.Create(request.price);
            if (priceResult.IsFailure) return Result.Failure<List<BookResponse>>(priceResult.Error);
            query = query.Where(b => b.Price == priceResult.Value);
        }

        var books = query.AsEnumerable();

        if (books is null)
            return Result.Failure<List<BookResponse>>(new Error(
                code: "Book.NotFound",
                message: "Book not found"));

        return books.Select(b => new BookResponse
        {
            Id = b.Id,
            Title = b.Title.Value,
            Author = b.Author.Value,
            Description = b.Description.Value,
            Price = b.Price.Value,
            Publisher = b.Publisher.Value,
        }).ToList();
    }
}
