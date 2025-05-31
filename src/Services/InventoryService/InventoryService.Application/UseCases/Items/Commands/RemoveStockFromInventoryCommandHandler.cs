using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Commands;

public record RemoveStockFromInventoryCommand(
    Guid productId,
    int Amount) : ICommand<int>;

public class RemoveStockFromInventoryCommandHandler(
    IStockBalanceRepository _stockBalanceRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<RemoveStockFromInventoryCommand, int>
{
    public async Task<Result<int>> Handle(RemoveStockFromInventoryCommand request, CancellationToken cancellationToken)
    {
        var product = await _stockBalanceRepository
            .SelectAsync(s => s.ProductId == request.productId && s.AvailableQuantity > request.Amount);

        if (product is null)
        {
            return Result.Failure<int>(new Error(
                code: "Stock.NotFound",
                message: $"This product is not found in your warehouse"));
        }

        product.RemoveQuantity(request.Amount);
        await _stockBalanceRepository.UpdateAsync(product);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(product.AvailableQuantity);
    }
}
