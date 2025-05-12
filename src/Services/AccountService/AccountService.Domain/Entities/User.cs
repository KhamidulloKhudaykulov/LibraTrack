using AccountService.Domain.Enums;
using AccountService.Domain.Events;
using AccountService.Domain.Primitives;
using AccountService.Domain.Shared;
using AccountService.Domain.ValueObjects.Users;

namespace AccountService.Domain.Entities;

public class User : Entity
{
    private User(
        FirstName firstName,
        LastName lastName,
        Email email,
        PhoneNumber phoneNumber,
        PassportNumber passportNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        PassportNumber = passportNumber;
    }

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public PassportNumber PassportNumber { get; private set; }
    public UserStatus UserStatus { get; private set; } = UserStatus.Active;

    public static Result<User> Create(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string passportNumber)
    {
        var user = new User
        (
            FirstName.Create(firstName).Value,
            LastName.Create(lastName).Value,
            Email.Create(email).Value,
            PhoneNumber.Create(phoneNumber).Value,
            PassportNumber.Create(passportNumber).Value
        );

        user.AddDomainEvent(new UserRegisteredDomainEvent(user.Id, user.Email.Value));

        return user;
    }

    public void ActiveUser() => UserStatus = UserStatus.Active;
    public void BlockUser()
    {
        UserStatus = UserStatus.Blocked;
        AddDomainEvent(new UserDeactivatedDomainEvent(Id));
    }
}
