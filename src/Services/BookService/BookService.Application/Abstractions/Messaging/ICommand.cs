using BookService.Domain.Shared;
using MediatR;

namespace BookService.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result> { }

public interface ICommand<TRequest> : IRequest<Result<TRequest>> { }
