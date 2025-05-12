using BookService.Domain.Primitives;
using BookService.Domain.Shared;

namespace BookService.Domain.ValueObjects.Books;

public class Price : ValueObject
{
    public decimal Value { get; }
    private Price(decimal value)
        => Value = value;

    public static Result<Price> Create(decimal value)
    {
        if (value < 0)
        {
            return Result.Failure<Price>(new Error(
                code: "Price.OutOfRange",
                message: "Price can't be less than 0 value"));
        }

        return new Price(value);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
