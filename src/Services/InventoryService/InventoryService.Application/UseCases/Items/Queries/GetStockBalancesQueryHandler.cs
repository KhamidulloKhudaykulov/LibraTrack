using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Application.Interfaces.Clients;
using InventoryService.Application.UseCases.Items.Contracts;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Queries;

public record GetStockBalancesQuery : IQuery<List<GetStockBalanceResponse>>;

public class GetStockBalancesQueryHandler(
    IStockBalanceRepository _stockBalanceRepository,
    IBookServiceClient _bookServiceClient) 
    : IQueryHandler<GetStockBalancesQuery, List<GetStockBalanceResponse>>
{
    public async Task<Result<List<GetStockBalanceResponse>>> Handle(GetStockBalancesQuery request, CancellationToken cancellationToken)
    {
        var products = await _stockBalanceRepository.SelectAllAsync();

        if (products.Any())
        {
            var distinctBookIds = (await _stockBalanceRepository.SelectAllAsync())
            .Select(x => x.ProductId).Distinct();
            var bookTitles = new Dictionary<Guid, string>();

            foreach (var bookId in distinctBookIds)
            {
                bookTitles[bookId] = await _bookServiceClient.GetBookNameAsync(bookId.ToString());
            }

            var result = products.Select(p => new GetStockBalanceResponse
            {
                Id = p.Id,
                ProductName = bookTitles[p.ProductId],
                AvailableQuantity = p.AvailableQuantity,
            });

            return Result.Success(result.ToList());
        }

        return Result.Failure<List<GetStockBalanceResponse>>(new Error(
            code: "List.Empty",
            message: "Products are not available in your inventory"));
    }
}
