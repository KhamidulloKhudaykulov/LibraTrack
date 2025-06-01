using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryService.Persistence.Repositories;

public class StockBalanceRepository : IStockBalanceRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<StockBalance> _stockBalances;

    public StockBalanceRepository(ApplicationDbContext context)
    {
        _context = context;
        _stockBalances = _context.Set<StockBalance>();
    }

    public async Task<StockBalance> InsertAsync(StockBalance stockBalance)
    {
        return (await _stockBalances.AddAsync(stockBalance)).Entity;
    }

    public async Task<StockBalance> UpdateAsync(StockBalance stockBalance)
    {
        return (await Task.FromResult(_stockBalances.Update(stockBalance))).Entity;
    }

    public async Task DeleteAsync(StockBalance stockBalance)
    {
        await Task.FromResult(_stockBalances.Remove(stockBalance));
    }

    public async Task<StockBalance> SelectAsync(Expression<Func<StockBalance, bool>> expression)
    {
        return await (
            expression is null 
            ? _stockBalances.FirstOrDefaultAsync() 
            : _stockBalances.FirstOrDefaultAsync(expression));
    }

    public async Task<IEnumerable<StockBalance>> SelectAllAsync(Expression<Func<StockBalance, bool>> expression = null)
    {
        return await Task.FromResult(
                expression is null 
                ? _stockBalances 
                : _stockBalances.Where(expression));
    }
}
