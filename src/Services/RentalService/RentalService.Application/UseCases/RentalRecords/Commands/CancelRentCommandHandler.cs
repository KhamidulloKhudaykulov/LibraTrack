using RentalService.Application.Abstractions.Messaging;
using RentalService.Application.UseCases.RentalRecords.Contracts;
using RentalService.Domain.Repositories;
using RentalService.Domain.Shared;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace RentalService.Application.UseCases.RentalRecords.Commands;

public record CancelRentCommand(
    Guid rentId) : ICommand<RentResultResponse>;

public class CancelRentCommandHandler(
    IRentalRecordRepository _rentalRecordRepository,
    IUnitOfWork _unitOfWork,
    HttpClient _httpClient) 
    : ICommandHandler<CancelRentCommand, RentResultResponse>
{
    public async Task<Result<RentResultResponse>> Handle(CancelRentCommand request, CancellationToken cancellationToken)
    {
        var rent = await _rentalRecordRepository.SelectAsync(r => r.Id == request.rentId && !r.IsDeleted);
        if (rent is null)
        {
            return Result.Failure<RentResultResponse>(new Error(
                code: "Rent.NotFound",
                message: $"This rent with ID={request.rentId} is not found or already has been canceled"));
        }

        rent.CancelRent();

        var requestToInventory = new ReceiveProductRequest { ProductId = rent.BookId, Amount = 1 };
        var content = new StringContent(
            JsonSerializer.Serialize(requestToInventory),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PutAsync("https://localhost:7096/api/inventory/add", content);
        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<RentResultResponse>(new Error(
                    code: response.StatusCode.ToString(),
                    message: response.RequestMessage.ToString()));
        }

        await _rentalRecordRepository.UpdateAsync(rent);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new RentResultResponse
        {
            Id = rent.Id,
            BookId = rent.BookId,
            UserId = rent.UserId,
            IsReturned = rent.IsReturned,
            IsPayed = rent.IsPayed,
            IsDeleted = rent.IsDeleted,
            Price = rent.RentPrice,
            StartDate = rent.StartDate.ToString("dd.MM.yyyy"),
            EndDate = rent.EndDate.ToString("dd.MM.yyyy"),
        };

        return Result.Success(result);
    }
}
