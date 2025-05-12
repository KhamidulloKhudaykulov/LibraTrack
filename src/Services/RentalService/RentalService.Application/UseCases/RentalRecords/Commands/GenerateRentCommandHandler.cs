using RentalService.Application.Abstractions.Messaging;
using RentalService.Application.UseCases.RentalRecords.Contracts;
using RentalService.Domain.Entities;
using RentalService.Domain.Repositories;
using RentalService.Domain.Shared;

namespace RentalService.Application.UseCases.RentalRecords.Commands;

public record GenerateRentCommand(
    Guid userId,
    Guid bookId,
    DateTime startDate,
    DateTime endDate) : ICommand<RentResultResponse>;

public class GenerateRentCommandHandler(
    IRentalRecordRepository _rentalRecordRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<GenerateRentCommand, RentResultResponse>
{
    public async Task<Result<RentResultResponse>> Handle(GenerateRentCommand request, CancellationToken cancellationToken)
    {
        var rent = RentalRecord.Create(
            request.userId,
            request.bookId,
            request.startDate,
            request.endDate,
            false).Value;

        try
        {
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
