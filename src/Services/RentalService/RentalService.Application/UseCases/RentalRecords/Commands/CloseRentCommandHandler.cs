using RentalService.Application.Abstractions.Messaging;
using RentalService.Application.UseCases.RentalRecords.Contracts;
using RentalService.Domain.Repositories;
using RentalService.Domain.Shared;
using System.Text;
using System.Text.Json;

namespace RentalService.Application.UseCases.RentalRecords.Commands;

public record CloseRentCommand(
    Guid rentId) : ICommand;

public class CloseRentCommandHandler(
    IRentalRecordRepository _rentalRecordRepository,
    IUnitOfWork _unitOfWork,
    HttpClient _httpClient)
    : ICommandHandler<CloseRentCommand>
{
    public async Task<Result> Handle(CloseRentCommand request, CancellationToken cancellationToken)
    {
        var rent = await _rentalRecordRepository.SelectAsync(r => r.Id == request.rentId && !r.IsDeleted);
        if (rent is null || rent.IsReturned)
        {
            return Result.Failure(new Error(
                code: "Rent.NotFound",
                message: $"This rent with ID={request.rentId} was not found or already closed or canceled"));
        }

        rent.CloseRent();
        rent.PayRent();

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

        return Result.Success();
    }
}
