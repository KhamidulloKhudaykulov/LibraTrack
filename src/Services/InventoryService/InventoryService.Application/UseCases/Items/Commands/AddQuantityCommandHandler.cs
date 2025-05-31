using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Commands;

public record AddQuantityCommand(
    Guid productId,
    int amount) : ICommand<int>;

public class AddQuantityCommandHandler(
    IStockBalanceRepository _stockBalanceRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<AddQuantityCommand, int>
{
    public async Task<Result<int>> Handle(AddQuantityCommand request, CancellationToken cancellationToken)
    {
        var existItemInRepository = await _stockBalanceRepository.SelectAsync(s => s.ProductId == request.productId);

        if (existItemInRepository is null)
        {
            return Result.Failure<int>(new Error(
                code: "Product.NotFound",
                message: $"This product with ID={request.productId} is not found"));
        }

        existItemInRepository.AddQuantity(request.amount);
        await _stockBalanceRepository.UpdateAsync(existItemInRepository);
        await _unitOfWork.SaveChangesAsync();

        return existItemInRepository.AvailableQuantity;
    }
}
