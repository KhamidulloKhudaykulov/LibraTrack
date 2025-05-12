using BookService.Domain.Entities;
using System.Linq.Expressions;

namespace BookService.Domain.Repositories;

public interface IBookRepository
{
    Task<Book> InsertAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task DeleteAsync(Book book);
    Task<Book> SelectAsync(Expression<Func<Book, bool>> expression);
    Task<IEnumerable<Book>> SelectAllAsync(Expression<Func<Book, bool>>? expression = null);
    Task<IQueryable<Book>> SelectAllAsQueryable(Expression<Func<Book, bool>>? expression = null);
}
