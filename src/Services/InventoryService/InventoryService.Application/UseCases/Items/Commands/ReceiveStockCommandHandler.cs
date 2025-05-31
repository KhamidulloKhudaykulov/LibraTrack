using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Application.Interfaces.Clients;
using InventoryService.Domain.Entities;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Commands;

public record ReceiveStockCommand(
    Guid productId,
    int amount,
    decimal price
    ) : ICommand<StockEntry>;

public class ReceiveStockCommandHandler(
    IStockEntryRepository _itemRepository,
    IStockBalanceRepository _stockBalanceRepository,
    IUnitOfWork _unitOfWork,
    IBookServiceClient _bookServiceClient) 
    : ICommandHandler<ReceiveStockCommand, StockEntry>
{
    public async Task<Result<StockEntry>> Handle(ReceiveStockCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookServiceClient.GetBookNameAsync(request.productId.ToString());
        if (book is null)
        {
            return Result.Failure<StockEntry>(new Error(
                code: "Product.NotFound",
                message: $"This product with ID={request.productId} is not found"));
        }

        var existProduct = await _stockBalanceRepository.SelectAsync(
            s => s.ProductId == request.productId);

        if (existProduct is not null)
        {
            existProduct.AddQuantity(request.amount);
        }
        else
        {
            var newStockBalance = StockBalance.Create(request.productId, request.amount).Value;
            await _stockBalanceRepository.InsertAsync(newStockBalance);
        }

        var newStock = StockEntry.Create(request.productId, request.amount, request.amount, request.price);

        await _itemRepository.InsertAsync(newStock.Value);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(newStock.Value);
    }
}
