using RentalService.Application.Abstractions.Messaging;
using RentalService.Application.UseCases.RentalRecords.Contracts;
using RentalService.Domain.Repositories;
using RentalService.Domain.Shared;

namespace RentalService.Application.UseCases.RentalRecords.Commands;

public record PayRentCommand(
    Guid rentId) : ICommand<RentResultResponse>;


public class PayRentCommandHandler(
    IRentalRecordRepository _rentalRecordRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<PayRentCommand, RentResultResponse>
{
    public async Task<Result<RentResultResponse>> Handle(PayRentCommand request, CancellationToken cancellationToken)
    {
        var rent = await _rentalRecordRepository.SelectAsync(r => r.Id == request.rentId && !r.IsDeleted);
        if (rent is null)
        {
            return Result.Failure<RentResultResponse>(new Error(
                code: "Rent.NotFound",
                message: $"This rent with ID={request.rentId} is not found or already has been canceled"));
        }

        rent.PayRent();
        await _rentalRecordRepository.UpdateAsync(rent);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var result = new RentResultResponse
        {
            UserId = rent.UserId,
            BookId = rent.BookId,
            StartDate = rent.StartDate.ToString("dd.MM.yyyy"),
            EndDate = rent.EndDate.ToString("dd.MM.yyyy"),
            Price = rent.RentPrice,
            IsPayed = rent.IsPayed,
            IsReturned = rent.IsReturned,
        };

        return Result.Success(result);
    }
}

