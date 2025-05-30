using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryService.Persistence.Repositories;

public class StockEntryRepository : IStockEntryRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<StockEntry> _items;

    public StockEntryRepository(ApplicationDbContext context)
    {
        _context = context;
        _items = _context.Set<StockEntry>();
    }

    public async Task<StockEntry> InsertAsync(StockEntry item)
    {
        return (await _items.AddAsync(item)).Entity;
    }

    public async Task<StockEntry> UpdateAsync(StockEntry item)
    {
        return (await Task.FromResult(_items.Update(item))).Entity;
    }

    public async Task DeleteAsync(StockEntry item)
    {
        await Task.FromResult(_items.Remove(item));
    }

    public async Task<StockEntry> SelectAsync(Expression<Func<StockEntry, bool>> expression)
    {
        return await _items.FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<StockEntry>> SelectAllAsync(Expression<Func<StockEntry, bool>>? expression = null)
    {
        var items = expression is null
            ? _items
            : _items
            .Where(expression);

        return await Task.FromResult(items);
    }
}
