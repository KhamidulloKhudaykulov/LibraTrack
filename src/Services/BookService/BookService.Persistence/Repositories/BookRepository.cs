using BookService.Domain.Entities;
using BookService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookService.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    public readonly ApplicationDbContext _context;
    public readonly DbSet<Book> _books;
    public BookRepository(ApplicationDbContext context)
    {
        _context = context;
        _books = _context.Set<Book>();
    }
    public async Task<Book> InsertAsync(Book book)
    {
        return (await _books.AddAsync(book)).Entity;
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        return (await Task.FromResult(_books.Update(book))).Entity;
    }

    public async Task DeleteAsync(Book book)
    {
        await Task.FromResult(_books.Remove(book));
    }

    public async Task<Book> SelectAsync(Expression<Func<Book, bool>> expression)
    {
        return await _books
            .FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<Book>> SelectAllAsync(Expression<Func<Book, bool>>? expression = null)
    {
        return await Task.FromResult(
            expression is null
            ? _books
            : _books.Where(expression));
    }

    public async Task<IQueryable<Book>> SelectAllAsQueryable(Expression<Func<Book, bool>>? expression = null)
    {
        return await Task.FromResult(
            expression is null
            ? _books.AsQueryable()
            : _books.Where(expression).AsQueryable());
    }
}
