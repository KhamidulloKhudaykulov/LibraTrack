using BookService.Domain.Primitives;
using BookService.Domain.Shared;

namespace BookService.Domain.ValueObjects.Books;

public class Author : ValueObject
{
    public string Value { get; }
    private Author(string value)
        => Value = value;

    public static Result<Author> Create(string value)
    {
        return new Author(value);
    }
    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
