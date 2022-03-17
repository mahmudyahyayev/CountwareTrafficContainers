#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Api/CountwareTraffic.Services.Area.Api.csproj", "src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Api/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Api/Mhd.Framework.Api.csproj", "src/BuildingBlocks/Mhd.Framework.Api/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Core/Mhd.Framework.Core.csproj", "src/BuildingBlocks/Mhd.Framework.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Ioc/Mhd.Framework.Ioc.csproj", "src/BuildingBlocks/Mhd.Framework.Ioc/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Grpc/Mhd.Framework.Grpc.csproj", "src/BuildingBlocks/Mhd.Framework.Grpc/"]
COPY ["src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Infrastructure/CountwareTraffic.Services.Area.Infrastructure.csproj", "src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Infrastructure/"]
COPY ["src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Application/CountwareTraffic.Services.Area.Application.csproj", "src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Application/"]
COPY ["src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Core/CountwareTraffic.Services.Area.Core.csproj", "src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Efcore/Mhd.Framework.Efcore.csproj", "src/BuildingBlocks/Mhd.Framework.Efcore/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Common/Mhd.Framework.Common.csproj", "src/BuildingBlocks/Mhd.Framework.Common/"]
COPY ["src/BuildingBlocks/Mhd.Framework.RabbitMq/Mhd.Framework.RabbitMq.csproj", "src/BuildingBlocks/Mhd.Framework.RabbitMq/"]
COPY ["src/BuildingBlocks/Mhd.Framework.QueueModel/Mhd.Framework.QueueModel.csproj", "src/BuildingBlocks/Mhd.Framework.QueueModel/"]
COPY ["src/CrossCuttingConcerns/AuditLog/AuditLog.csproj", "src/CrossCuttingConcerns/AuditLog/"]
COPY ["src/CrossCuttingConcerns/Observability/Observability.csproj", "src/CrossCuttingConcerns/Observability/"]
RUN dotnet restore "src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Api/CountwareTraffic.Services.Area.Api.csproj"
COPY . .
WORKDIR "/src/src/CountwareTraffic.Services.Area/CountwareTraffic.Services.Area.Api"
RUN dotnet build "CountwareTraffic.Services.Area.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Area.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Area.Api.dll"]
