#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Events.Listener2/CountwareTraffic.Services.Events.Listener2.csproj", "src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Events.Listener2/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Ioc/Mhd.Framework.Ioc.csproj", "src/BuildingBlocks/Mhd.Framework.Ioc/"]
COPY ["src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Event.Infrastructure/CountwareTraffic.Services.Event.Infrastructure.csproj", "src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Event.Infrastructure/"]
COPY ["src/BuildingBlocks/Mhd.Framework.ElasticSearch/Mhd.Framework.ElasticSearch.csproj", "src/BuildingBlocks/Mhd.Framework.ElasticSearch/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Core/Mhd.Framework.Core.csproj", "src/BuildingBlocks/Mhd.Framework.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Common/Mhd.Framework.Common.csproj", "src/BuildingBlocks/Mhd.Framework.Common/"]
COPY ["src/CrossCuttingConcerns/AuditLog/AuditLog.csproj", "src/CrossCuttingConcerns/AuditLog/"]
COPY ["src/CrossCuttingConcerns/Observability/Observability.csproj", "src/CrossCuttingConcerns/Observability/"]
COPY ["src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Event.Application/CountwareTraffic.Services.Event.Application.csproj", "src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Event.Application/"]
COPY ["src/BuildingBlocks/Mhd.Framework.RabbitMq/Mhd.Framework.RabbitMq.csproj", "src/BuildingBlocks/Mhd.Framework.RabbitMq/"]
COPY ["src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Event.Core/CountwareTraffic.Services.Event.Core.csproj", "src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Event.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Efcore/Mhd.Framework.Efcore.csproj", "src/BuildingBlocks/Mhd.Framework.Efcore/"]
COPY ["src/BuildingBlocks/Mhd.Framework.QueueModel/Mhd.Framework.QueueModel.csproj", "src/BuildingBlocks/Mhd.Framework.QueueModel/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Scheduler/Mhd.Framework.Scheduler.csproj", "src/BuildingBlocks/Mhd.Framework.Scheduler/"]
RUN dotnet restore "src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Events.Listener2/CountwareTraffic.Services.Events.Listener2.csproj"
COPY . .
WORKDIR "/src/src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Events.Listener2"
RUN dotnet build "CountwareTraffic.Services.Events.Listener2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Events.Listener2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Events.Listener2.dll"]
