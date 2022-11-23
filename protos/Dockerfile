FROM --platform=linux/amd64 namely/protoc-all:1.50_0

ENV NODE_VERSION=18
ENV DEBIAN_VERSION=11
ENV DOTNET_VERSION=6.0
ENV GRPC_WEB_VERSION=1.4.2

# install dependencies
RUN curl -fsSL https://deb.nodesource.com/setup_${NODE_VERSION}.x | bash - \
  && curl https://packages.microsoft.com/config/debian/${DEBIAN_VERSION}/packages-microsoft-prod.deb --output packages-microsoft-prod.deb \
  && dpkg -i packages-microsoft-prod.deb \
  && rm packages-microsoft-prod.deb \
  && apt-get update \
  && apt-get install -y nodejs dotnet-sdk-${DOTNET_VERSION} \
  && npm install -g yarn

RUN curl https://github.com/grpc/grpc-web/releases/download/${GRPC_WEB_VERSION}/protoc-gen-grpc-web-${GRPC_WEB_VERSION}-linux-x86_64 --output protoc-gen-grpc-web \
  && mkdir -p /root/.bin/ \
  && mv protoc-gen-grpc-web /root/.bin/

WORKDIR /proto
ADD src/ src/
ADD generated/ generated/
RUN dotnet restore generated/csharp/dev.zico.protobuf.csproj \
  && cd generated/js && yarn install
COPY build.sh ./

ENV PROTOC_GEN_TS_PATH="/proto/generated/js/node_modules/.bin/protoc-gen-ts"
ENV PROTOC_GEN_CSHARP_PATH="/root/.nuget/packages/grpc.tools/2.49.0/tools/linux_x64/grpc_csharp_plugin"
ENV PROTOC_GEN_WEB_PATH="/root/.bin/protoc-gen-grpc-web"
RUN ./build.sh