using BookService.Domain.Shared;
using MediatR;

namespace BookService.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
