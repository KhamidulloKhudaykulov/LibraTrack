using AdminPanel.Api.Entities;
using System.Linq.Expressions;

namespace AdminPanel.Api.Persistence.Repositories;

public interface IAdminRepository
{
    Task<Admin> InsertAsync(Admin admin);
    Task<Admin> UpdateAsync(Admin admin);
    Task DeleteAsync(Admin admin);
    Task<IEnumerable<Admin>> SelectAllAsync(Expression<Func<Admin, bool>> expression);
    Task<int> SaveChangesAsync();
}
