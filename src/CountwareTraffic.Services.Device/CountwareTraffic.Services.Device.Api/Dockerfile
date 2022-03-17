#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Api/CountwareTraffic.Services.Device.Api.csproj", "src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Api/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Api/Mhd.Framework.Api.csproj", "src/BuildingBlocks/Mhd.Framework.Api/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Core/Mhd.Framework.Core.csproj", "src/BuildingBlocks/Mhd.Framework.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Ioc/Mhd.Framework.Ioc.csproj", "src/BuildingBlocks/Mhd.Framework.Ioc/"]
COPY ["src/CrossCuttingConcerns/Observability/Observability.csproj", "src/CrossCuttingConcerns/Observability/"]
COPY ["src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Infrastructure/CountwareTraffic.Services.Device.Infrastructure.csproj", "src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Infrastructure/"]
COPY ["src/CrossCuttingConcerns/AuditLog/AuditLog.csproj", "src/CrossCuttingConcerns/AuditLog/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Common/Mhd.Framework.Common.csproj", "src/BuildingBlocks/Mhd.Framework.Common/"]
COPY ["src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Application/CountwareTraffic.Services.Device.Application.csproj", "src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Application/"]
COPY ["src/BuildingBlocks/Mhd.Framework.RabbitMq/Mhd.Framework.RabbitMq.csproj", "src/BuildingBlocks/Mhd.Framework.RabbitMq/"]
COPY ["src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Core/CountwareTraffic.Services.Device.Core.csproj", "src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Efcore/Mhd.Framework.Efcore.csproj", "src/BuildingBlocks/Mhd.Framework.Efcore/"]
COPY ["src/BuildingBlocks/Mhd.Framework.QueueModel/Mhd.Framework.QueueModel.csproj", "src/BuildingBlocks/Mhd.Framework.QueueModel/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Grpc/Mhd.Framework.Grpc.csproj", "src/BuildingBlocks/Mhd.Framework.Grpc/"]
RUN dotnet restore "src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Api/CountwareTraffic.Services.Device.Api.csproj"
COPY . .
WORKDIR "/src/src/CountwareTraffic.Services.Device/CountwareTraffic.Services.Device.Api"
RUN dotnet build "CountwareTraffic.Services.Device.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Device.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Device.Api.dll"]
