using InventoryService.Domain.Shared;
using MediatR;

namespace InventoryService.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
