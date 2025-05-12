namespace RentalService.Application.Common;

public interface IRentalOutboxProcessorJob
{
    Task ProcessAsync(CancellationToken token);
}
