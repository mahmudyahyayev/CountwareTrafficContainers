#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.Services.Devices.Api/CountwareTraffic.Services.Devices.Api.csproj", "CountwareTraffic.Services.Devices.Api/"]
COPY ["CountwareTraffic.Services.Devices.Infrastructure/CountwareTraffic.Services.Devices.Infrastructure.csproj", "CountwareTraffic.Services.Devices.Infrastructure/"]
COPY ["AuditLog/AuditLog.csproj", "AuditLog/"]
COPY ["CountwareTraffic.Services.Devices.Application/CountwareTraffic.Services.Devices.Application.csproj", "CountwareTraffic.Services.Devices.Application/"]
COPY ["CountwareTraffic.Services.Devices.Core/CountwareTraffic.Services.Devices.Core.csproj", "CountwareTraffic.Services.Devices.Core/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.Efcore/Sensormatic.Tool.Efcore.csproj", "Sensormatic.Tool.Efcore/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
COPY ["Sensormatic.Tool.QueueModel/Sensormatic.Tool.QueueModel.csproj", "Sensormatic.Tool.QueueModel/"]
COPY ["Sensormatic.Tool.Queue/Sensormatic.Tool.Queue.csproj", "Sensormatic.Tool.Queue/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
COPY ["Sensormatic.Tool.Api/Sensormatic.Tool.Api.csproj", "Sensormatic.Tool.Api/"]
COPY ["Sensormatic.Tool.Grpc/Sensormatic.Tool.Grpc.csproj", "Sensormatic.Tool.Grpc/"]
RUN dotnet restore "CountwareTraffic.Services.Devices.Api/CountwareTraffic.Services.Devices.Api.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.Services.Devices.Api"
RUN dotnet build "CountwareTraffic.Services.Devices.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Devices.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Devices.Api.dll"]