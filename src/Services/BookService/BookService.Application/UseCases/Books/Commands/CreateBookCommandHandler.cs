using BookService.Application.Abstractions.Messaging;
using BookService.Domain.Entities;
using BookService.Domain.Repositories;
using BookService.Domain.Shared;

namespace BookService.Application.UseCases.Books.Commands;

public record CreateBookCommand(
    string title,
    string description,
    string author,
    string publisher,
    decimal price) : ICommand<Guid>;

public class CreateBookCommandHandler(
    IBookRepository _bookRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<CreateBookCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = Book.Create(request.title, request.description, request.author, request.publisher, request.price).Value;

        await _bookRepository.InsertAsync(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
