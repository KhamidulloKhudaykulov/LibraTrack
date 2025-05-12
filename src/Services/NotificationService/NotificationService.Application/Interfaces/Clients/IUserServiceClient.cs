namespace NotificationService.Application.Interfaces.Clients;

public interface IUserServiceClient
{
    Task<string> GetUserEmailAsync(string userId);
}
