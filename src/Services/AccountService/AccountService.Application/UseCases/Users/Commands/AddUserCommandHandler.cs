using AccountService.Application.Abstractions.Messaging;
using AccountService.Domain.Entities;
using AccountService.Domain.Repositories;
using AccountService.Domain.Shared;

namespace AccountService.Application.UseCases.Users.Commands;

public record AddUserCommand(
    string firstName,
    string lastName,
    string email,
    string phoneNumber,
    string passportNumber) : ICommand<Guid>;

public sealed class AddUserCommandHandlerHandler(
    IUserRepository _userRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<AddUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = (await _userRepository
            .SelectAllAsync())
            .FirstOrDefault(
                u => u.Email.Value == request.email ||
                u.PhoneNumber.Value == request.phoneNumber ||
                u.PassportNumber.Value == request.passportNumber);

        if (existUser is not null)
        {
            return Result.Failure<Guid>(new Error(
                code: "User.Exists",
                message: "This user is already added"));
        }

        var user = User.Create(request.firstName, request.lastName, request.email, request.phoneNumber, request.passportNumber).Value;

        try
        {
            await _userRepository.InsertAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return Result.Failure<Guid>(new Error(
                code: "Register.DatabaseError",
                message: $"An error occurred while saving data: {ex.Message}"));
        }

        return user.Id;
    }
}