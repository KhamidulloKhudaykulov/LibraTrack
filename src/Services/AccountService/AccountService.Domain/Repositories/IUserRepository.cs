using AccountService.Domain.Entities;
using System.Linq.Expressions;

namespace AccountService.Domain.Repositories;

public interface IUserRepository
{
    Task<User> InsertAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<User> SelectAsync(Expression<Func<User, bool>> expression);
    Task<IEnumerable<User>> SelectAllAsync(Expression<Func<User, bool>>? expression = null);
}
