using RentalService.Application.Abstractions.Messaging;
using RentalService.Application.Interfaces.Clients;
using RentalService.Application.UseCases.RentalRecords.Contracts;
using RentalService.Domain.Entities;
using RentalService.Domain.Repositories;
using RentalService.Domain.Shared;

namespace RentalService.Application.UseCases.RentalRecords.Queries;

public record GetAllRentsQuery : IQuery<List<RentResultResponse>>;

public class GetAllRentsQueryHandler(
    IRentalRecordRepository _rentalRecordRepository,
    IBookServiceClient _bookServiceClient,
    IUserServiceClient _userServiceClient) : IQueryHandler<GetAllRentsQuery, List<RentResultResponse>>
{
    public async Task<Result<List<RentResultResponse>>> Handle(GetAllRentsQuery request, CancellationToken cancellationToken)
    {
        var distinctBookIds = (await _rentalRecordRepository.SelectAllAsync())
            .Select(x => x.BookId).Distinct();
        var bookTitles = new Dictionary<Guid, string>();

        foreach (var bookId in distinctBookIds)
        {
            bookTitles[bookId] = await _bookServiceClient.GetBookNameAsync(bookId.ToString());
        }

        var distinctUserIds = (await _rentalRecordRepository.SelectAllAsync())
            .Select(x => x.UserId).Distinct();
        var userNames = new Dictionary<Guid, string>();

        foreach (var userId in distinctUserIds)
        {
            userNames[userId] = await _userServiceClient.GetUserNameAsync(userId.ToString());
        }

        var rents = (await _rentalRecordRepository
            .SelectAllAsync())
            .Select(r => new RentResultResponse
            {
                Id = r.Id,
                BookId = r.BookId,
                BookTitle = bookTitles[r.BookId],
                StartDate = r.StartDate.ToString("dd.MM.yyyy"),
                EndDate = r.EndDate.ToString("dd.MM.yyyy"),
                UserName = userNames[r.UserId],
                Price = r.RentPrice,
                IsPayed = r.IsPayed,
                IsReturned = r.IsReturned,
                IsDeleted = r.IsDeleted,
            });

        return Result.Success(rents.ToList());
    }
}
