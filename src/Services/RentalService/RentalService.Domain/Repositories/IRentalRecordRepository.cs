using RentalService.Domain.Entities;
using System.Linq.Expressions;

namespace RentalService.Domain.Repositories;

public interface IRentalRecordRepository
{
    Task<RentalRecord> InsertAsync(RentalRecord record);
    Task<RentalRecord> UpdateAsync(RentalRecord record);
    Task DeleteAsync(RentalRecord rentalRecord);
    Task<RentalRecord> SelectAsync(Expression<Func<RentalRecord, bool>> expression);
    Task<IEnumerable<RentalRecord>> SelectAllAsync(Expression<Func<RentalRecord, bool>>? expression = null);
    Task<IQueryable<RentalRecord>> SelectAllAsQueryableAsync(Expression<Func<RentalRecord, bool>>? expression = null);
}
