using AccountService.Application.Abstractions.Messaging;
using AccountService.Domain.Entities;
using AccountService.Domain.Repositories;
using AccountService.Domain.Shared;

namespace AccountService.Application.UseCases.Users.Commands;

public record UpdateUserCommand(
    Guid id,
    string firstName = "",
    string lastName = "",
    string email = "",
    string passportNumber = "",
    string phoneNumber = "") : ICommand<User>;

public class UpdateUserCommandHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<UpdateUserCommand, User>
{
    public async Task<Result<User>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SelectAsync(u => u.Id == request.id);

        if (user is null)
        {
            return Result.Failure<User>(new Error(
                code: "User.NotFound",
                message: $"This user with ID={request.id} is not found"));
        }

        user.Update(
            request.firstName,
            request.lastName,
            request.email,
            request.phoneNumber,
            request.passportNumber);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(user);
    }
}
