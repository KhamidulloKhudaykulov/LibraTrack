using InventoryService.Domain.Repositories;

namespace InventoryService.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await _context.SaveChangesAsync(token);
    }
}
