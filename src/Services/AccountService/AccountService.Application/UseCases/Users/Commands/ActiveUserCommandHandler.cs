using AccountService.Application.Abstractions.Messaging;
using AccountService.Domain.Enums;
using AccountService.Domain.Repositories;
using AccountService.Domain.Shared;

namespace AccountService.Application.UseCases.Users.Commands;

public record ActiveUserCommand(
    Guid id) : ICommand<bool>;

public class ActiveUserCommandHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<ActiveUserCommand, bool>
{
    public async Task<Result<bool>> Handle(ActiveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SelectAsync(u => u.Id == request.id);
        if (user is null)
        {
            return Result.Failure<bool>(new Error(
                   code: "User.NotFound",
                   message: "This user is not found"));
        }

        if (user.UserStatus == UserStatus.Active)
        {
            return Result.Failure<bool>(new Error(
                   code: "User.AlreadyActive",
                   message: "This user is already active"));
        }

        user.ActiveUser();
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
