using AccountService.Domain.Entities;
using AccountService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AccountService.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<User> _users;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }
    public async Task<User> InsertAsync(User user)
    {
        return (await _users.AddAsync(user)).Entity;
    }

    public async Task<User> UpdateAsync(User user)
    {
        return (await Task.FromResult(_users.Update(user))).Entity;
    }

    public async Task DeleteAsync(User user)
    {
        await Task.FromResult(_users.Remove(user));
    }

    public async Task<User> SelectAsync(Expression<Func<User, bool>> expression)
    {
        return await _users.FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<User>> SelectAllAsync(Expression<Func<User, bool>>? expression = null)
    {
        var users = expression is null
            ? _users
            : _users
            .Where(expression);

        return await Task.FromResult(users);
    }
}
