using AccountService.Domain.Primitives;
using AccountService.Domain.Shared;

namespace AccountService.Domain.ValueObjects.Users;

public class LastName : ValueObject
{
    public LastName(string value)
        => Value = value;

    public string Value { get; }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<LastName> Create(string value)
    {
        if (value is null || value == string.Empty)
        {
            return Result.Failure<LastName>(
                new Error(
                    code: "FirstName.NullValue",
                    message: "The specific value can't be null"));
        }

        return new LastName(value);
    }
}
