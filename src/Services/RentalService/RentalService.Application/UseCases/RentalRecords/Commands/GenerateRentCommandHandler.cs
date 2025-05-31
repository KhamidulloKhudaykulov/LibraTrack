using RentalService.Application.Abstractions.Messaging;
using RentalService.Application.UseCases.RentalRecords.Contracts;
using RentalService.Application.UseCases.RentalRecords.Queries;
using RentalService.Domain.Entities;
using RentalService.Domain.Repositories;
using RentalService.Domain.Shared;
using System.Text;
using System.Text.Json;

namespace RentalService.Application.UseCases.RentalRecords.Commands;

public record GenerateRentCommand(
    Guid userId,
    Guid bookId,
    DateTime startDate,
    DateTime endDate,
    decimal price) : ICommand<RentResultResponse>;

public class GenerateRentCommandHandler(
    IRentalRecordRepository _rentalRecordRepository,
    IUnitOfWork _unitOfWork,
    HttpClient _httpClient) : ICommandHandler<GenerateRentCommand, RentResultResponse>
{
    public async Task<Result<RentResultResponse>> Handle(GenerateRentCommand request, CancellationToken cancellationToken)
    {
        var rent = RentalRecord.Create(
            request.userId,
            request.bookId,
            request.startDate,
            request.endDate,
            request.price,
            false,
            false).Value;

        try
        {
            var requestToInventory = new DeductProductRequest { ProductId = request.bookId, Amount = 1 };
            var content = new StringContent(
                JsonSerializer.Serialize(requestToInventory),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PutAsync("https://localhost:7096/api/inventory/deduct", content);
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var errorResponse = JsonSerializer.Deserialize<Error>(
                        responseContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                    );

                return Result.Failure<RentResultResponse>(new Error(
                    code: errorResponse!.Code.ToString(),
                    message: errorResponse?.Message ?? "Unknown error"));
            }

            await _rentalRecordRepository.InsertAsync(rent);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            return Result.Failure<RentResultResponse>(new Error(
                code: "Something went wrong!",
                message: ex.Message.ToString()));
        }

        var result = new RentResultResponse
        {
            UserId = request.userId,
            BookId = request.bookId,
            StartDate = request.startDate.ToString("dd.MM.yyyy"),
            EndDate = request.endDate.ToString("dd.MM.yyyy")
        };

        return Result.Success(result);
    }
}
