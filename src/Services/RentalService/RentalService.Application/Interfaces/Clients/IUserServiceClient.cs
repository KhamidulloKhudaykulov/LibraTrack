namespace RentalService.Application.Interfaces.Clients;

public interface IUserServiceClient
{
    Task<string> GetUserEmailAsync(string userId);
}
