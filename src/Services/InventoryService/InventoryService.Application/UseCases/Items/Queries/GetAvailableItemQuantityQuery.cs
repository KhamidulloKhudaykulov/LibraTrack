using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Queries;

public record GetAvailableItemQuantityQuery(
    Guid productId) : IQuery<int>;

public class GetAvailableItemQuantityQueryHandler(
    IItemRepository _itemRepository) : IQueryHandler<GetAvailableItemQuantityQuery, int>
{
    public async Task<Result<int>> Handle(GetAvailableItemQuantityQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.SelectAsync(i => i.ProductId == request.productId);
        if (item is null)
        {
            return Result.Failure<int>(new Error(
                code: "Item.NotFound",
                message: $"This item with ID={request.productId} is not found"));
        }

        return Result.Success(item.Amount);
    }
}
