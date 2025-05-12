using BookService.Domain.Primitives;
using BookService.Domain.Shared;
using BookService.Domain.ValueObjects.Books;

namespace BookService.Domain.Entities;

public class Book : Entity
{
    private Book(
        Title title,
        Description description,
        Author author,
        Publisher publisher,
        Price price)
    {
        Title = title;
        Description = description;
        Author = author;
        Publisher = publisher;
        Price = price;
    }

    public Title Title { get; private set; }
    public Description Description { get; private set; }
    public Author Author { get; private set; }
    public Publisher Publisher { get; private set; }
    public Price Price { get; private set; }

    public static Result<Book> Create(
        string title,
        string description,
        string author,
        string publisher,
        decimal price)
    {
        return new Book(
            Title.Create(title).Value,
            Description.Create(description).Value,
            Author.Create(author).Value,
            Publisher.Create(publisher).Value,
            Price.Create(price).Value);
    }
}
