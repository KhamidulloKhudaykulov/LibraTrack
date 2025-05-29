using InventoryService.Application.Abstractions.Messaging;
using InventoryService.Application.Interfaces.Clients;
using InventoryService.Application.UseCases.Items.Contracts;
using InventoryService.Domain.Repositories;
using InventoryService.Domain.Shared;

namespace InventoryService.Application.UseCases.Items.Queries;

public class GetItemsFromInventoryQuery : IQuery<List<GetItemsResponse>>;

public class GetItemsFromInventoryQueryHandler(
    IItemRepository _itemRepository,
    IBookServiceClient _bookServiceClient) 
    : IQueryHandler<GetItemsFromInventoryQuery, List<GetItemsResponse>>
{
    public async Task<Result<List<GetItemsResponse>>> Handle(GetItemsFromInventoryQuery request, CancellationToken cancellationToken)
    {
        var distinctBookIds = (await _itemRepository.SelectAllAsync())
            .Select(x => x.ProductId).Distinct();
        var bookTitles = new Dictionary<Guid, string>();

        foreach (var bookId in distinctBookIds)
        {
            bookTitles[bookId] = await _bookServiceClient.GetBookNameAsync(bookId.ToString());
        }

        var items = await _itemRepository.SelectAllAsync();

        var result = items.Select(i => new GetItemsResponse
        {
            Id = i.Id,
            ProductName = bookTitles[i.ProductId],
            AvailableQuantity = i.AvailableQuantity,
            Amount = i.Amount,
            Price = i.Price,
            TotalPrice = i.TotalPrice,
            CreatedDate = i.CreatedAt.ToString("dd.MM.yyyy"),
        }).ToList();

        return Result.Success(result);
    }
}
