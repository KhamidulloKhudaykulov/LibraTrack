using AccountService.Domain.Shared;
using MediatR;

namespace AccountService.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }
