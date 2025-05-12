using Grpc.Core;
using Grpc.Net.Client;
using RentalService.Application.Interfaces.Clients;

namespace RentalService.Infrastructure.Grpc.Clients;

public class UserServiceClient : IUserServiceClient
{
    private readonly UserService.UserServiceClient _client;
    public UserServiceClient()
    {
        var channel = GrpcChannel.ForAddress("https://localhost:7287");
        _client = new UserService.UserServiceClient(channel);
    }

    public async Task<string> GetUserEmailAsync(string userId = "")
    {
        GetUserEmailRequest? request;
        if (string.IsNullOrEmpty(userId))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter user id"));

        request = new GetUserEmailRequest { UserId = userId };

        var result = await _client.GetUserEmailAsync(request);
        return result.Email;
    }
}
