using BookService.Application.Abstractions.Messaging;
using BookService.Application.UseCases.Books.Contracts;
using BookService.Domain.Repositories;
using BookService.Domain.Shared;
using BookService.Domain.ValueObjects.Books;

namespace BookService.Application.UseCases.Books.Queries;

public record GetBookQuery(
    Guid id,
    string title = "",
    string author = "",
    string publisher = "",
    decimal price = 0) : IQuery<BookResponse>;

public class GetBookQueryHandler(
    IBookRepository _bookRepository)
    : IQueryHandler<GetBookQuery, BookResponse>
{
    public async Task<Result<BookResponse>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        var query = await _bookRepository.SelectAllAsQueryable();

        if (request.id != Guid.Empty)
        {
            query = query.Where(b => b.Id == request.id);
        }

        if (!string.IsNullOrWhiteSpace(request.title))
        {
            var titleResult = Title.Create(request.title);
            if (titleResult.IsFailure) return Result.Failure<BookResponse>(titleResult.Error);
            query = query.Where(b => b.Title.Value.ToLower().Contains(titleResult.Value.Value.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(request.author))
        {
            var authorResult = Author.Create(request.author);
            if (authorResult.IsFailure) return Result.Failure<BookResponse>(authorResult.Error);
            query = query.Where(b => b.Author.Value.ToLower().Contains(authorResult.Value.Value.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(request.publisher))
        {
            var publisherResult = Publisher.Create(request.publisher);
            if (publisherResult.IsFailure) return Result.Failure<BookResponse>(publisherResult.Error);
            query = query.Where(b => b.Publisher.Value.ToLower().Contains(publisherResult.Value.Value.ToLower()));
        }

        if (request.price > 0)
        {
            var priceResult = Price.Create(request.price);
            if (priceResult.IsFailure) return Result.Failure<BookResponse>(priceResult.Error);
            query = query.Where(b => b.Price == priceResult.Value);
        }

        var book = query.FirstOrDefault();

        if (book is null)
            return Result.Failure<BookResponse>(new Error(
                code: "Book.NotFound",
                message: "Book not found"));

        return Result.Success(new BookResponse
        {
            Id = book.Id,
            Title = book.Title.Value,
            Author = book.Author.Value,
            Description = book.Description.Value,
            Publisher = book.Publisher.Value,
            Price = book.Price.Value
        });
    }
}
