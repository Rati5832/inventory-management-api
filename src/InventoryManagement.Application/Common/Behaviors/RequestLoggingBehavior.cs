using MediatR;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Common.Behaviors;

public class RequestLoggingBehavior<TRequest, TResponse>(ILogger<RequestLoggingBehavior<TRequest, TResponse>> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        logger.LogInformation("Handling application request {RequestName}", requestName);

        var response = await next();

        logger.LogInformation("Completed application request {RequestName}", requestName);
        return response;
    }
}
