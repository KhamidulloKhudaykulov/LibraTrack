using BookService.Application.Abstractions.Messaging;
using BookService.Domain.Repositories;
using BookService.Domain.Shared;

namespace BookService.Application.UseCases.Books.Commands;

public record DeleteBookCommand(
    Guid id) : ICommand<bool>;

public class DeleteBookCommandHandler(
    IBookRepository _bookRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteBookCommand, bool>
{
    public async Task<Result<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var result = await _bookRepository.SelectAsync(b => b.Id == request.id);
        if (result is null)
        {
            return Result.Failure<bool>(new Error(
                code: "Book.NotFound",
                message: "This Book is not found"));
        }

        await _bookRepository.DeleteAsync(result);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
