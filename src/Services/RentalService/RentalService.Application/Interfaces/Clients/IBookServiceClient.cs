namespace RentalService.Application.Interfaces.Clients;

public interface IBookServiceClient
{
    Task<string> GetBookNameAsync(string bookId);
}
