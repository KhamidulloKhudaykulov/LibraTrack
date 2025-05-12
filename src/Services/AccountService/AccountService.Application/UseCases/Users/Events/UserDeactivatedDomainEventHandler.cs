using AccountService.Domain.Events;
using AccountService.Domain.Repositories;
using AccountService.Domain.Shared;
using MediatR;

namespace AccountService.Application.UseCases.Users.Events;

public class UserDeactivatedDomainEventHandler : INotificationHandler<UserDeactivatedDomainEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserDeactivatedDomainEventHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task Handle(UserDeactivatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.SelectAsync(u => u.Id == notification.UserId);
        if (user is null)
        {
            Console.WriteLine($"This user with ID={notification.UserId} was not found");
            return;
        }

        user.BlockUser();

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
