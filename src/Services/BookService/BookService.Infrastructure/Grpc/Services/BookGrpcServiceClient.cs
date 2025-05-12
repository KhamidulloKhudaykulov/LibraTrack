using BookService.Domain.Repositories;
using Grpc.Core;

namespace BookService.Infrastructure.Grpc.Services;

public class BookGrpcServiceClient(IBookRepository _bookRepository)
    : BookService.BookServiceBase
{
    public override async Task<BookNameResponse> GetBookName(GetBookNameRequest request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.BookId))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter book id"));

        var book = await _bookRepository.SelectAsync(
            b => b.Id == Guid.Parse(request.BookId));

        if (book is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Book not found"));

        return new BookNameResponse
        {
            Bookname = book.Title.Value.ToString()
        };
    }
}
