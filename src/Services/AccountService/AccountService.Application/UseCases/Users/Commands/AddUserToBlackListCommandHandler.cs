using AccountService.Application.Abstractions.Messaging;
using AccountService.Domain.Enums;
using AccountService.Domain.Repositories;
using AccountService.Domain.Shared;

namespace AccountService.Application.UseCases.Users.Commands;

public record AddUserToBlackListCommand(
    Guid id) : ICommand<bool>;

public class AddUserToBlackListCommandHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<AddUserToBlackListCommand, bool>
{
    public async Task<Result<bool>> Handle(AddUserToBlackListCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SelectAsync(u => u.Id == request.id);
        if (user is null)
        {
            return Result.Failure<bool>(new Error(
                   code: "User.NotFound",
                   message: "This user is not found"));
        }

        if (user.UserStatus == UserStatus.Blocked)
        {
            return Result.Failure<bool>(new Error(
                   code: "User.AlreadyBlocked",
                   message: "This user is already blocked"));
        }

        user.BlockUser();
        try
        {
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<bool>(new Error(
                   code: "500",
                   message: ex.Message.ToString()));
        }

        return true;
    }
}
