#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.Services.Users.Api/CountwareTraffic.Services.Users.Api.csproj", "CountwareTraffic.Services.Users.Api/"]
COPY ["CountwareTraffic.Services.Users.Application/CountwareTraffic.Services.Users.Application.csproj", "CountwareTraffic.Services.Users.Application/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
COPY ["Sensormatic.Tool.Grpc/Sensormatic.Tool.Grpc.csproj", "Sensormatic.Tool.Grpc/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
RUN dotnet restore "CountwareTraffic.Services.Users.Api/CountwareTraffic.Services.Users.Api.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.Services.Users.Api"
RUN dotnet build "CountwareTraffic.Services.Users.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Users.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Users.Api.dll"]