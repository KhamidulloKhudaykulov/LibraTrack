using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Commands;

public record DeductItemFromStockCommand(
    Guid productId,
    int amount) : ICommand<Item>;

public class DeductItemFromStockCommandHandler(
    IItemRepository _itemRepository,
    IUnitOfWork _unitOfWork) 
    : ICommandHandler<DeductItemFromStockCommand, Item>
{
    public async Task<Result<Item>> Handle(DeductItemFromStockCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.SelectAsync(i => i.ProductId == request.productId);
        if (item is null)
        {
            return Result.Failure<Item>(new Error(
                code: "Product.NotFound",
                message: $"This product with ID={request.productId} is not found"));
        }

        if (item.AvailableQuantity == 0)
        {
            return Result.Failure<Item>(new Error(
                code: "Product.NotAvailable",
                message: $"This product with ID={request.productId} is not available in your warehouse"));
        }

        if (item.RemoveAvailableQuantity(request.amount) < 0)
        {
            return Result.Failure<Item>(new Error(
                code: "Product.NotAvailable",
                message: $"There is no such quantity of products available in your warehouse"));
        }

        await _itemRepository.UpdateAsync(item);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return item;
    }
}
