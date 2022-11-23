import { GreeterClient } from "dev.zico.protobuf/src/HelloWorldServiceClientPb";

type GrpcServiceClient = typeof GreeterClient;

function useGrpcClient(client: GrpcServiceClient) {
    const instance = new client("http://localhost:8080"); 

    return instance;
}

export {useGrpcClient};
