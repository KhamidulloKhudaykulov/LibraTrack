using BookService.Domain.Primitives;
using BookService.Domain.Shared;

namespace BookService.Domain.ValueObjects.Books;

public class Description : ValueObject
{
    public string Value { get; }
    private Description(string value)
        => Value = value;

    public static Result<Description> Create(string value)
    {
        return new Description(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
