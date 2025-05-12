using AccountService.Domain.Primitives;
using AccountService.Domain.Shared;

namespace AccountService.Domain.ValueObjects.Users;

public class FirstName : ValueObject
{
    public FirstName(string value)
        => Value = value;

    public string Value { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<FirstName> Create(string value)
    {
        if (value is null || value == string.Empty)
        {
            return Result.Failure<FirstName>(
                new Error(
                    code: "FirstName.NullValue",
                    message: "The specific value can't be null"));
        }

        return new FirstName(value);
    }
}
