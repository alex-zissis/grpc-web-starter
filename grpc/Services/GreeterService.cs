using System;
using System.Threading;
using Grpc.Core;
using dev.zico.protobuf;

namespace grpc.Services;

public class GreeterService : dev.zico.protobuf.Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext serverCallContext)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
