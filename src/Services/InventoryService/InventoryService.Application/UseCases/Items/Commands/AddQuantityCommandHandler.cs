using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Commands;

public record AddQuantityCommand(
    Guid productId,
    int amount) : ICommand<int>;

public class AddQuantityCommandHandler(
    IItemRepository _itemRepository,
    IUnitOfWork _unitOfWork) : ICommandHandler<AddQuantityCommand, int>
{
    public async Task<Result<int>> Handle(AddQuantityCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.SelectAsync(i => i.ProductId == request.productId);
        if (item is null)
        {
            return Result.Failure<int>(new Error(
                code: "Product.NotFound",
                message: $"This product with ID={request.productId} is not found"));
        }

        item.AddAvailableQuantity(request.amount);
        await _itemRepository.UpdateAsync(item);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(item.AvailableQuantity);
    }
}
