using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Commands;

public record ReceiveStockCommand(
    Guid productId,
    int amount,
    decimal price
    ) : ICommand<Item>;

public class ReceiveStockCommandHandler(
    IItemRepository _itemRepository,
    IUnitOfWork _unitOfWork) 
    : ICommandHandler<ReceiveStockCommand, Item>
{
    public async Task<Result<Item>> Handle(ReceiveStockCommand request, CancellationToken cancellationToken)
    {
        var stock = await _itemRepository.SelectAsync(i => i.ProductId == request.productId);

        if (stock is not null)
        {
            stock.AddAvailableQuantity(request.amount);

            await _itemRepository.UpdateAsync(stock);
            await _unitOfWork.SaveChangesAsync();

            return stock;
        }

        var newStock = Item.Create(request.productId, request.amount, request.amount, request.price);

        await _itemRepository.InsertAsync(newStock.Value);
        await _unitOfWork.SaveChangesAsync();

        return newStock;
    }
}
