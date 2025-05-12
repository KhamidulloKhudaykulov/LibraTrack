using BookService.Domain.Primitives;
using BookService.Domain.Shared;

namespace BookService.Domain.ValueObjects.Books;

public class Publisher : ValueObject
{
    public string Value { get; }
    private Publisher(string value)
        => Value = value;

    public static Result<Publisher> Create(string value)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= 0)
        {
            return Result.Failure<Publisher>(new Error(
                code: "Booktitle.NullOrEmpty",
                message: "The specific value can't be null or empty"));
        }

        if (value.Length > 50)
        {
            return Result.Failure<Publisher>(new Error(
               code: "Booktitle.NullOrEmpty",
               message: "The specific value can't be longer than 50 chars"));
        }

        return new Publisher(value);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
