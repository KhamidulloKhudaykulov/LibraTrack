using BookService.Application.Abstractions.Messaging;
using BookService.Domain.Repositories;
using BookService.Domain.Shared;

namespace BookService.Application.UseCases.Books.Commands;

public record UpdateBookCommand(
    Guid id,
    string title = "",
    string description = "",
    string author = "",
    string publisher = "",
    decimal price = 0) : ICommand<Guid>;

public class UpdateBookCommandHandler(
    IBookRepository _bookRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateBookCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.SelectAsync(b => b.Id == request.id);
        if (book is null)
        {
            return Result.Failure<Guid>(new Error(
                code: "Book.NotFound",
                message: $"Book with ID={request.id} is not found"));
        }
        
        var updateResult = book.Update(
            request.title,
            request.description,
            request.author,
            request.publisher,
            request.price);

        if (updateResult.IsFailure)
        {
            return Result.Failure<Guid>(updateResult.Error);
        }

        await _bookRepository.UpdateAsync(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book.Id;
    }
}
