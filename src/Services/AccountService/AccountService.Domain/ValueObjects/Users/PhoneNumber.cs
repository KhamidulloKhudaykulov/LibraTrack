using AccountService.Domain.Primitives;
using AccountService.Domain.Shared;
using System.Text.RegularExpressions;

namespace AccountService.Domain.ValueObjects.Users;

public class PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public static Result<PhoneNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<PhoneNumber>(
                new Error("PhoneNumber.Empty", "Telefon raqam bo‘sh bo‘lishi mumkin emas."));
        }

        // Regex: +998901234567 yoki 998901234567
        var uzbekPhonePattern = @"^(\+998|998)(\d{9})$";
        if (!Regex.IsMatch(value, uzbekPhonePattern))
        {
            return Result.Failure<PhoneNumber>(
                new Error("PhoneNumber.InvalidFormat", "Telefon raqam formati noto‘g‘ri. (+998901234567)"));
        }

        return Result.Success(new PhoneNumber(value));
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
