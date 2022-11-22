FROM namely/protoc-all:1.50_0

RUN curl -fsSL https://deb.nodesource.com/setup_18.x | bash - \
  && curl https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb --output packages-microsoft-prod.deb \
  && dpkg -i packages-microsoft-prod.deb \
  && rm packages-microsoft-prod.deb \
  && apt-get update \
  && apt-get install -y nodejs dotnet-sdk-6.0 \
  && npm install -g yarn

WORKDIR /proto
ADD src/ src/
ADD generated/ generated/
RUN dotnet restore generated/csharp/dev.zico.protobuf.csproj \
  && cd generated/js && yarn install
COPY build.sh ./

ENV PROTOC_GEN_TS_PATH="/proto/generated/js/node_modules/.bin/protoc-gen-ts"
ENV PROTOC_GEN_CSHARP_PATH="/root/.nuget/packages/grpc.tools/2.49.0/tools/linux_x64/grpc_csharp_plugin"
RUN ./build.sh