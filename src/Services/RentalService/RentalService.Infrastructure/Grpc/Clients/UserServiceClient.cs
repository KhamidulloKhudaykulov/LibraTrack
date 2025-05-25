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

    public async Task<string> GetUserNameAsync(string userId = "")
    {
        GetUserNameRequest? request;
        if (string.IsNullOrEmpty(userId))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter user id"));

        request = new GetUserNameRequest { UserId = userId };

        var result = await _client.GetUserNameAsync(request);
        return result.Name;
    }
}
