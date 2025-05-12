using RentalService.Application.Abstractions.Messaging;
using RentalService.Domain.Repositories;
using RentalService.Domain.Shared;

namespace RentalService.Application.UseCases.RentalRecords.Commands;

public record CloseRentCommand(
    Guid rentId) : ICommand;

public class CloseRentCommandHandler
    (IRentalRecordRepository _rentalRecordRepository,
    IUnitOfWork _unitOfWork)
    : ICommandHandler<CloseRentCommand>
{
    public async Task<Result> Handle(CloseRentCommand request, CancellationToken cancellationToken)
    {
        var rent = await _rentalRecordRepository.SelectAsync(b => b.Id == request.rentId);
        if (rent is null || rent.IsReturned)
        {
            return Result.Failure(new Error(
                code: "Rent.NotFound",
                message: $"This rent with ID={request.rentId} was not found or already closed"));
        }

        rent.CloseRent();
        await _rentalRecordRepository.UpdateAsync(rent);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
