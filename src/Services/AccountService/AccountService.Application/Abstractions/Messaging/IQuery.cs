using AccountService.Domain.Shared;
using MediatR;

namespace AccountService.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
