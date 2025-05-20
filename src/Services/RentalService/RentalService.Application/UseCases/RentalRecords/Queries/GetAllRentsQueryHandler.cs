using RentalService.Application.Abstractions.Messaging;
using RentalService.Application.Interfaces.Clients;
using RentalService.Application.UseCases.RentalRecords.Contracts;
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
        var rents = (await _rentalRecordRepository
            .SelectAllAsync())
            .Select(async r => new RentResultResponse
            {
                BookId = r.BookId,
                BookTitle = await _bookServiceClient.GetBookNameAsync(r.BookId.ToString()),
                StartDate = r.StartDate.ToString("dd.MM.yyyy"),
                EndDate = r.EndDate.ToString("dd.MM.yyyy"),
                UserEmail = await _userServiceClient.GetUserEmailAsync(r.UserId.ToString())
            });

        return (await Task.WhenAll(rents)).ToList();
    }
}
