using InventoryService.Domain.Entities;
using System.Linq.Expressions;

namespace InventoryService.Domain.Repositories;

public interface IItemRepository
{
    Task<Item> InsertAsync(Item item);
    Task<Item> UpdateAsync(Item item);
    Task DeleteAsync(Item item);
    Task<Item> SelectAsync(Expression<Func<Item, bool>> expression);
    Task<IEnumerable<Item>> SelectAllAsync(Expression<Func<Item, bool>>? expression = null);
}
