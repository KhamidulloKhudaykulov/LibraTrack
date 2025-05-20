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

    public Result<User> Update(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        string passportNumber)
    {
        var firstNameResult = FirstName.Create(firstName);
        var lastNameResult = LastName.Create(lastName);
        var emailResult = Email.Create(email);
        var passportNumberResult = PassportNumber.Create(passportNumber);
        var phoneNumberResult = PhoneNumber.Create(phoneNumber);

        if (firstNameResult.IsFailure)
            return Result.Failure<User>(firstNameResult.Error);
        if (lastNameResult.IsFailure)
            return Result.Failure<User>(lastNameResult.Error);
        if (emailResult.IsFailure)
            return Result.Failure<User>(emailResult.Error);
        if (phoneNumberResult.IsFailure)
            return Result.Failure<User>(phoneNumberResult.Error);
        if (passportNumberResult.IsFailure)
            return Result.Failure<User>(passportNumberResult.Error);

        FirstName = firstNameResult.Value;
        LastName = lastNameResult.Value;
        Email = emailResult.Value;
        PassportNumber = passportNumberResult.Value;
        PhoneNumber = phoneNumberResult.Value;

        return Result.Success(this);
    }

    public void ActiveUser() => UserStatus = UserStatus.Active;
    public void BlockUser()
    {
        UserStatus = UserStatus.Blocked;
        AddDomainEvent(new UserDeactivatedDomainEvent(Id));
    }
}
