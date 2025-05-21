using Grpc.Core;
using Grpc.Net.Client;
using InventoryService.Application.Interfaces.Clients;
using InventoryService.Infrastructure.Grpc;

namespace NotificationService.Infrastructure.Grpc.Clients;

public class BookServiceClient : IBookServiceClient
{
    private readonly BookService.BookServiceClient _client;
    public BookServiceClient()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7202");
        _client = new BookService.BookServiceClient(channel);
    }
    public async Task<string> GetBookNameAsync(string bookId)
    {
        GetBookNameRequest? request;
        if (string.IsNullOrEmpty(bookId))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter book id"));

        request = new GetBookNameRequest { BookId = bookId };

        var result = await _client.GetBookNameAsync(request);

        return result.Bookname.ToString();
    }
}
