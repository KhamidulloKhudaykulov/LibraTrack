using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryService.Persistence.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Item> _items;

    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
        _items = _context.Set<Item>();
    }

    public async Task<Item> InsertAsync(Item item)
    {
        return (await _items.AddAsync(item)).Entity;
    }

    public async Task<Item> UpdateAsync(Item item)
    {
        return (await Task.FromResult(_items.Update(item))).Entity;
    }

    public async Task DeleteAsync(Item item)
    {
        await Task.FromResult(_items.Remove(item));
    }

    public async Task<Item> SelectAsync(Expression<Func<Item, bool>> expression)
    {
        return await _items.FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<Item>> SelectAllAsync(Expression<Func<Item, bool>>? expression = null)
    {
        var items = expression is null
            ? _items
            : _items
            .Where(expression);

        return await Task.FromResult(items);
    }
}
