using InventoryService.Domain.Entities;
using System.Linq.Expressions;

namespace InventoryService.Domain.Repositories;

public interface IStockBalanceRepository
{
    Task<StockBalance> InsertAsync(StockBalance stockBalance);
    Task<StockBalance> UpdateAsync(StockBalance stockBalance);
    Task DeleteAsync(StockBalance stockBalance);
    Task<StockBalance> SelectAsync(Expression<Func<StockBalance, bool>> expression);
    Task<IEnumerable<StockBalance>> SelectAllAsync(Expression<Func<StockBalance, bool>> expression = null);
}
