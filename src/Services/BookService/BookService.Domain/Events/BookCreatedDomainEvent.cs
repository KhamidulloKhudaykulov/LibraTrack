using BookService.Domain.Primitives;
using MediatR;

namespace BookService.Domain.Events;

public class BookCreatedDomainEvent : DomainEvent, INotification
{
    public Guid BookId { get; set; }
    public BookCreatedDomainEvent(Guid bookId)
    {
        BookId = bookId;
    }
}
