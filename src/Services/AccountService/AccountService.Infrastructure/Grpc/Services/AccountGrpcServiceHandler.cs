using AccountService.Domain.Repositories;
using Grpc.Core;

namespace AccountService.Infrastructure.Grpc.Services;

public class AccountGrpcServiceHandler(IUserRepository _userRepository)
    : UserService.UserServiceBase
{
    public override async Task<UserEmailResponse> GetUserEmail(GetUserEmailRequest request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.UserId))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Enter user id"));

        var user = await _userRepository.SelectAsync(
            u => u.Id == Guid.Parse(request.UserId));

        if (user is null)
            throw new RpcException(new Status(StatusCode.NotFound, "User not found"));

        return new UserEmailResponse
        {
            Email = user.Email.Value
        };
    }
}
