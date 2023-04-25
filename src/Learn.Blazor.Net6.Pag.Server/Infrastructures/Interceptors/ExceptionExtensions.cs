using Grpc.Core;
using Microsoft.Data.SqlClient;

namespace Learn.Blazor.Net6.Pag.Server.Infrastructures.Interceptors;

public static class ExceptionExtensions
{
    public static RpcException Handle<T>(this Exception exception,  ILogger<T> logger, Guid correlationId) =>
        exception switch
        {
            TimeoutException ex => HandleException(ex, logger, correlationId),
            SqlException ex => HandleException(ex, logger, correlationId),
            RpcException ex => HandleException(ex, logger, correlationId),
            _ => HandleException(exception, logger, correlationId)
        };

    private static RpcException Handle<T>(this Exception ex, ILogger<T> logger, Status status, Metadata trailers)
    {
        var correlationId = trailers.Last();
        logger.LogError(ex, $"{correlationId.Key}: {correlationId.Value} - {status.Detail}");
        return new RpcException(status, trailers);
    }

    private static RpcException HandleException<T>(TimeoutException ex, ILogger<T> logger, Guid correlationId)
    {
        var status = new Status(StatusCode.Internal, "An external resource did not answer within the time limit");
        return ex.Handle(logger, status, CreateTrailers(correlationId));
    }

    private static RpcException HandleException<T>(SqlException ex, ILogger<T> logger, Guid correlationId)
    {
        var status = ex.Number == -2 
            ? new Status(StatusCode.DeadlineExceeded, "SQL timeout") 
            : new Status(StatusCode.Internal, "SQL error");
        return ex.Handle(logger, status, CreateTrailers(correlationId));
    }

    private static RpcException HandleException<T>(RpcException ex, ILogger<T> logger, Guid correlationId)
    {
        var status = new Status(ex.StatusCode, "An error occurred");
        var trailers = ex.Trailers;
        trailers.Add(CreateTrailers(correlationId).First());
        return ex.Handle(logger, status, trailers);
    }

    private static RpcException HandleException<T>(Exception ex, ILogger<T> logger, Guid correlationId)
    {
        var status = new Status(StatusCode.Internal, ex.Message);
        return ex.Handle(logger, status, CreateTrailers(correlationId));
    }

    private static Metadata CreateTrailers(Guid correlationId) => 
        new() { { "CorrelationId", correlationId.ToString() } };
}