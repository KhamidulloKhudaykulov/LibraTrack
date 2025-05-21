using InventoryService.Domain.Shared;
using MediatR;

namespace InventoryService.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }
