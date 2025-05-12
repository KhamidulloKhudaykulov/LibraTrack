using RentalService.Domain.Events;
using RentalService.Domain.Primitives;
using RentalService.Domain.Shared;

namespace RentalService.Domain.Entities;

public class RentalRecord : Entity
{
    private RentalRecord(
        Guid userId,
        Guid bookId,
        DateTime startDate,
        DateTime endDate,
        bool isReturned)
    {
        UserId = userId;
        BookId = bookId;
        StartDate = startDate;
        EndDate = endDate;
        IsReturned = isReturned;
    }

    public Guid UserId { get; private set; }
    public Guid BookId { get; private set; }
    public DateTime StartDate { get; private set; } = DateTime.MinValue;
    public DateTime EndDate { get; private set; } = DateTime.MinValue;
    public bool IsReturned { get; private set; }

    public void CloseRent()
    {
        IsReturned = false;
        AddDomainEvent(new RentClosedDomainEvent(Id, UserId, BookId));
    }

    public static Result<RentalRecord> Create(
        Guid userId,
        Guid bookId,
        DateTime startDate,
        DateTime endDate,
        bool isReturned)
    {
        if (userId == Guid.Empty)
        {
            return Result.Failure<RentalRecord>(new Error(
                code: "UserId.NullOrEmpty",
                message: "User can't be null or empty"));
        }

        if (bookId == Guid.Empty)
        {
            return Result.Failure<RentalRecord>(new Error(
                code: "BookId.NullOrEmpty",
                message: "Book can't be null or empty"));
        }

        if (startDate == DateTime.MinValue)
        {
            return Result.Failure<RentalRecord>(new Error(
                code: "StartDate.NullOrEmpty",
                message: "Start date can't be null or empty"));
        }

        if (endDate == DateTime.MinValue)
        {
            return Result.Failure<RentalRecord>(new Error(
                code: "EndDate.NullOrEmpty",
                message: "End date can't be null or empty"));
        }

        var rentalRecord = new RentalRecord(userId,
        bookId,
        startDate,
        endDate,
        isReturned);

        rentalRecord.AddDomainEvent(new RentGeneratedDomainEvent(
            bookId,
            userId,
            startDate.ToString("dd.MM.yyyy"),
            endDate.ToString("dd.MM.yyyy")));

        return rentalRecord;
    }
}
