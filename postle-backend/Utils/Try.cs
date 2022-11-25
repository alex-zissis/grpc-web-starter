using Grpc.Core;

namespace Postle.Web.Utils;

public static class Try
{
    public static T To<T>(Func<T> func, Action<Exception>? exceptionInterceptor = null)
    {
        try
        {
            return func();
        }
        catch (Exception exception)
        {
            exceptionInterceptor?.Invoke(exception);

            throw new RpcException(new Status(StatusCode.Unknown,
                $"Internal error occured in rpc call execution: {exception.Message}"));
        }
    }
}
