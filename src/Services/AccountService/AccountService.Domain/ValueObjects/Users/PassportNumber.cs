using AccountService.Domain.Primitives;
using AccountService.Domain.Shared;
using System.Text.RegularExpressions;

namespace AccountService.Domain.ValueObjects.Users;

public class PassportNumber : ValueObject
{
    public string Value { get; }

    private PassportNumber(string value)
    {
        Value = value;
    }

    public static Result<PassportNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<PassportNumber>(
                new Error("PassportNumber.Empty", "Pasport raqami bo‘sh bo‘lishi mumkin emas."));
        }

        // Regex: 2 ta katta harf + 7 ta raqam (AA1234567)
        var pattern = @"^[A-Z]{2}\d{7}$";
        if (!Regex.IsMatch(value, pattern))
        {
            return Result.Failure<PassportNumber>(
                new Error("PassportNumber.InvalidFormat", "Pasport raqami noto‘g‘ri formatda (masalan: AA1234567)."));
        }

        return new PassportNumber(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
