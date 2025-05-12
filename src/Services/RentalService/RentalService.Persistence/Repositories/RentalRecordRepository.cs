using Microsoft.EntityFrameworkCore;
using RentalService.Domain.Entities;
using RentalService.Domain.Repositories;
using System.Linq.Expressions;

namespace RentalService.Persistence.Repositories;

public class RentalRecordRepository : IRentalRecordRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<RentalRecord> _rentals;

    public RentalRecordRepository(ApplicationDbContext context)
    {
        _context = context;
        _rentals = _context.Set<RentalRecord>();
    }

    public async Task<RentalRecord> InsertAsync(RentalRecord rentalRecord)
    {
        return (await _rentals.AddAsync(rentalRecord)).Entity;
    }

    public async Task<RentalRecord> UpdateAsync(RentalRecord rentalRecord)
    {
        return (await Task.FromResult(_rentals.Update(rentalRecord))).Entity;
    }

    public async Task DeleteAsync(RentalRecord rentalRecord)
    {
        await Task.FromResult(_rentals.Remove(rentalRecord));
    }

    public async Task<RentalRecord> SelectAsync(Expression<Func<RentalRecord, bool>> expression)
    {
        return await _rentals
            .FirstOrDefaultAsync(expression);
    }

    public async Task<IEnumerable<RentalRecord>> SelectAllAsync(Expression<Func<RentalRecord, bool>>? expression = null)
    {
        return await Task.FromResult(
            expression is null
            ? _rentals
            : _rentals.Where(expression));
    }

    public async Task<IQueryable<RentalRecord>> SelectAllAsQueryableAsync(Expression<Func<RentalRecord, bool>>? expression = null)
    {
        return (await Task.FromResult(
            expression is null
            ? _rentals
            : _rentals.Where(expression)))
            .AsQueryable();
    }
}
