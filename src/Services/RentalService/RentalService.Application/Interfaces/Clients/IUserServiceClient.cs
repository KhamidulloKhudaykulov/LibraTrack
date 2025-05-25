namespace RentalService.Application.Interfaces.Clients;

public interface IUserServiceClient
{
    Task<string> GetUserNameAsync(string userId);
}
