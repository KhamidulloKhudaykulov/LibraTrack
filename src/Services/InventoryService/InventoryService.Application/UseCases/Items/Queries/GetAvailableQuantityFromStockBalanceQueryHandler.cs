using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Queries;

public record GetAvailableQuantityFromStockBalanceQuery(
    Guid productId) 
    : ICommand<int>;

public class GetAvailableQuantityFromStockBalanceQueryHandler(
    IStockBalanceRepository _stockBalanceRepository) 
    : ICommandHandler<GetAvailableQuantityFromStockBalanceQuery, int>
{
    public async Task<Result<int>> Handle(GetAvailableQuantityFromStockBalanceQuery request, CancellationToken cancellationToken)
    {
        var product = await _stockBalanceRepository.SelectAsync(s => s.ProductId == request.productId);
        if (product is null)
        {
            return Result.Failure<int>(new Error(
                code: "Product.NotFound",
                message: $"This product with ID={request.productId} is not found"));
        }

        return product.AvailableQuantity;
    }
}
