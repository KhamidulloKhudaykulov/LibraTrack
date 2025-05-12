using MediatR;
using RentalService.Domain.Shared;

namespace RentalService.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }
