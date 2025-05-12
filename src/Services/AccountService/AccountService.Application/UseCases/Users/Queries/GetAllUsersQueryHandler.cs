using AccountService.Application.Abstractions.Messaging;
using AccountService.Application.UseCases.Users.Contracts;
using AccountService.Domain.Repositories;
using AccountService.Domain.Shared;

namespace AccountService.Application.UseCases.Users.Queries;

public record GetAllUsersQuery : IQuery<List<UserResponse>>;
public class GetAllUsersQueryHandler(
    IUserRepository _userRepository) : IQueryHandler<GetAllUsersQuery, List<UserResponse>>
{
    public async Task<Result<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        return (await _userRepository.SelectAllAsync())
            .Select(u => new UserResponse
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
