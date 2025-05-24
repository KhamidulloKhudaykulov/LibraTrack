using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryService.Persistence.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Item> _items;

    public ItemRepository(ApplicationDbContext context, DbSet<Item> items)
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

    public Task<Item> SelectAsync(Expression<Func<Item, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Item>> SelectAllAsync(Expression<Func<Item, bool>>? expression = null)
    {
        throw new NotImplementedException();
    }
}
