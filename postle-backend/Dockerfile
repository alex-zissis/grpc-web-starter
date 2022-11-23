# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
RUN mkdir postle-backend
COPY Postle.sln .
COPY postle-backend/Postle.Web.csproj postle-backend/Postle.Web.csproj

# copy the protos
RUN mkdir -p protos/generated/csharp
COPY ./protos/generated/csharp protos/generated/csharp

# copy everything else and build app
COPY . .
RUN dotnet restore
RUN dotnet publish postle-backend/Postle.Web.csproj -c release -o /app

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "Postle.Web.dll"]
