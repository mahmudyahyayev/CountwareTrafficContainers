#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.WorkerServices.Audit.Consumer/CountwareTraffic.WorkerServices.Audit.Consumer.csproj", "CountwareTraffic.WorkerServices.Audit.Consumer/"]
COPY ["CountwareTraffic.WorkerServices.Audit.Application/CountwareTraffic.WorkerServices.Audit.Application.csproj", "CountwareTraffic.WorkerServices.Audit.Application/"]
COPY ["CountwareTraffic.WorkerServices.Audit.Data/CountwareTraffic.WorkerServices.Audit.Data.csproj", "CountwareTraffic.WorkerServices.Audit.Data/"]
COPY ["Sensormatic.Tool.MongoDb/Sensormatic.Tool.MongoDb.csproj", "Sensormatic.Tool.MongoDb/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.QueueModel/Sensormatic.Tool.QueueModel.csproj", "Sensormatic.Tool.QueueModel/"]
COPY ["Sensormatic.Tool.Queue/Sensormatic.Tool.Queue.csproj", "Sensormatic.Tool.Queue/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
COPY ["AuditLog/AuditLog.csproj", "AuditLog/"]
RUN dotnet restore "CountwareTraffic.WorkerServices.Audit.Consumer/CountwareTraffic.WorkerServices.Audit.Consumer.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.WorkerServices.Audit.Consumer"
RUN dotnet build "CountwareTraffic.WorkerServices.Audit.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.WorkerServices.Audit.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.WorkerServices.Audit.Consumer.dll"]
