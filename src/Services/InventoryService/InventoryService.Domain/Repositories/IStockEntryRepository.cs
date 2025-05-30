using InventoryService.Domain.Entities;
using System.Linq.Expressions;

namespace InventoryService.Domain.Repositories;

public interface IStockEntryRepository
{
    Task<StockEntry> InsertAsync(StockEntry item);
    Task<StockEntry> UpdateAsync(StockEntry item);
    Task DeleteAsync(StockEntry item);
    Task<StockEntry> SelectAsync(Expression<Func<StockEntry, bool>> expression);
    Task<IEnumerable<StockEntry>> SelectAllAsync(Expression<Func<StockEntry, bool>>? expression = null);
}
