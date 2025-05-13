using AdminPanel.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AdminPanel.Api.Persistence.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Admin> _admins;

    public AdminRepository(ApplicationDbContext context)
    {
        _context = context;
        _admins = _context.Set<Admin>();
    }


    public async Task DeleteAsync(Admin admin)
    {
        await Task.FromResult(_admins.Remove(admin));
    }

    public async Task<Admin> InsertAsync(Admin admin)
    {
        return (await _admins.AddAsync(admin)).Entity;
    }

    public async Task<IEnumerable<Admin>> SelectAllAsync(Expression<Func<Admin, bool>> expression)
    {
        var admins = expression is null
            ? _admins
            : _admins
            .Where(expression);

        return await Task.FromResult(admins.AsEnumerable());
    }

    public async Task<Admin> UpdateAsync(Admin admin)
    {
        return (await Task.FromResult(_admins.Update(admin))).Entity;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
