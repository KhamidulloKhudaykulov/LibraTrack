using AccountService.Application.Abstractions.Messaging;
using AccountService.Application.UseCases.Users.Contracts;
using AccountService.Domain.Repositories;
using AccountService.Domain.Shared;
using AccountService.Domain.ValueObjects.Users;

namespace AccountService.Application.UseCases.Users.Queries;

public record GetUserQuery(
    string firstName = "",
    string lastName = "",
    string passportNumber = "") : IQuery<List<UserResponse>>;

public class GetUserQueryHandler(
    IUserRepository _userRepository) : IQueryHandler<GetUserQuery, List<UserResponse>>
{
    public async Task<Result<List<UserResponse>>> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.passportNumber)
            && string.IsNullOrEmpty(request.firstName)
            && string.IsNullOrEmpty(request.lastName))
        {
            return Result.Failure<List<UserResponse>>(new Error(
                    code: "User.NotFound",
                    message: "Please fill the information"));
        }

        if (!string.IsNullOrEmpty(request.firstName) && !string.IsNullOrEmpty(request.lastName))
        {
            var pattern = $"{request.firstName}{request.lastName}".ToUpper();
            var patternAsc = $"{request.lastName}{request.firstName}".ToUpper();
            var users = (await _userRepository
                .SelectAllAsync()).ToList()
                .Where(u => $"{u.FirstName.Value}{u.LastName.Value}".ToUpper().Contains(pattern) ||
                            $"{u.LastName.Value}{u.FirstName.Value}".ToUpper().Contains(pattern) ||
                            $"{u.FirstName.Value}{u.LastName.Value}".ToUpper().Contains(patternAsc) ||
                            $"{u.LastName.Value}{u.FirstName.Value}".ToUpper().Contains(patternAsc));

            if (users is not null)
            {
                if (!string.IsNullOrEmpty(request.passportNumber))
                {
                    users = users.Where(u => u.PassportNumber.Value == request.passportNumber);
                    if (!users.Any())
                    {
                        return Result.Failure<List<UserResponse>>(new Error(
                            code: "User.NotFound",
                            message: "This user is not found"));
                    }
                }
                return users.Select(u => new UserResponse
                {
                    Id = u.Id,
                    FullName = $"{u.FirstName.Value} {u.LastName.Value}",
                    Email = u.Email.Value,
                    PassportNumber = u.PassportNumber.Value,
                    PhoneNumber = u.PhoneNumber.Value,
                    Status = u.UserStatus.ToString(),
                }).ToList();
            }
        }

        if (!string.IsNullOrEmpty(request.passportNumber))
        {
            var passportNumberValue = PassportNumber.Create(request.passportNumber).Value;
            var users = await _userRepository.SelectAllAsync(u => u.PassportNumber == passportNumberValue);
            if (users is null)
            {
                return Result.Failure<List<UserResponse>>(new Error(
                    code: "User.NotFound",
                    message: "This user is not found"));
            }

            var userResponses = users.Select(u => new UserResponse
            {
                Id = u.Id,
                FullName = $"{u.FirstName.Value} {u.LastName.Value}",
                Email = u.Email.Value,
                PassportNumber = u.PassportNumber.Value,
                PhoneNumber = u.PhoneNumber.Value,
                Status = u.UserStatus.ToString(),
            }).ToList();

            return userResponses;
        }

        if (!string.IsNullOrEmpty(request.firstName))
        {
            var users = (await _userRepository.SelectAllAsync())
                .Where(u => $"{u.FirstName.Value}".Contains(request.firstName));
            if (users is null)
            {
                return Result.Failure<List<UserResponse>>(new Error(
                    code: "User.NotFound",
                    message: "This user is not found"));
            }

            var userResponses = users.Select(u => new UserResponse
            {
                Id = u.Id,
                FullName = $"{u.FirstName.Value} {u.LastName.Value}",
                Email = u.Email.Value,
                PassportNumber = u.PassportNumber.Value,
                PhoneNumber = u.PhoneNumber.Value,
                Status = u.UserStatus.ToString(),
            }).ToList();

            return userResponses;
        }

        if (!string.IsNullOrEmpty(request.lastName))
        {
            var users = (await _userRepository.SelectAllAsync())
                .Where(u => $"{u.LastName.Value}".Contains(request.firstName));
            if (users is null)
            {
                return Result.Failure<List<UserResponse>>(new Error(
                    code: "User.NotFound",
                    message: "This user is not found"));
            }

            var userResponses = users.Select(u => new UserResponse
            {
                Id = u.Id,
                FullName = $"{u.FirstName.Value} {u.LastName.Value}",
                Email = u.Email.Value,
                PassportNumber = u.PassportNumber.Value,
                PhoneNumber = u.PhoneNumber.Value,
                Status = u.UserStatus.ToString(),
            }).ToList();

            return userResponses;
        }

        return Result.Failure<List<UserResponse>>(new Error(
                    code: "User.NotFound",
                    message: "This user is not found"));
    }
}
