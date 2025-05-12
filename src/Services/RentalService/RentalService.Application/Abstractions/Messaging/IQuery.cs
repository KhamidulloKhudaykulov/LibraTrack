using MediatR;
using RentalService.Domain.Shared;

namespace RentalService.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
