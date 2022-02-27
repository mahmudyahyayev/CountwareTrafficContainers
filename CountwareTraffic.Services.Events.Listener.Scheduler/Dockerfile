#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.Services.Events.Listener.Scheduler/CountwareTraffic.Services.Events.Listener.Scheduler.csproj", "CountwareTraffic.Services.Events.Listener.Scheduler/"]
COPY ["Sensormatic.Tool.Scheduler/Sensormatic.Tool.Scheduler.csproj", "Sensormatic.Tool.Scheduler/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
COPY ["CountwareTraffic.Services.Events.Infrastructure/CountwareTraffic.Services.Events.Infrastructure.csproj", "CountwareTraffic.Services.Events.Infrastructure/"]
COPY ["AuditLog/AuditLog.csproj", "AuditLog/"]
COPY ["CountwareTraffic.Services.Events.Application/CountwareTraffic.Services.Events.Application.csproj", "CountwareTraffic.Services.Events.Application/"]
COPY ["Sensormatic.Tool.QueueModel/Sensormatic.Tool.QueueModel.csproj", "Sensormatic.Tool.QueueModel/"]
COPY ["Sensormatic.Tool.Queue/Sensormatic.Tool.Queue.csproj", "Sensormatic.Tool.Queue/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
COPY ["CountwareTraffic.Services.Events.Core/CountwareTraffic.Services.Events.Core.csproj", "CountwareTraffic.Services.Events.Core/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.Efcore/Sensormatic.Tool.Efcore.csproj", "Sensormatic.Tool.Efcore/"]
COPY ["Sensormatic.Tool.ElasticSearch/Sensormatic.Tool.ElasticSearch.csproj", "Sensormatic.Tool.ElasticSearch/"]
RUN dotnet restore "CountwareTraffic.Services.Events.Listener.Scheduler/CountwareTraffic.Services.Events.Listener.Scheduler.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.Services.Events.Listener.Scheduler"
RUN dotnet build "CountwareTraffic.Services.Events.Listener.Scheduler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Events.Listener.Scheduler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Events.Listener.Scheduler.dll"]
