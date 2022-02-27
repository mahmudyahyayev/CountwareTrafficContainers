#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.WorkerServices.Sms.Consumer/CountwareTraffic.WorkerServices.Sms.Consumer.csproj", "CountwareTraffic.WorkerServices.Sms.Consumer/"]
COPY ["CountwareTraffic.WorkerServices.Sms.Application/CountwareTraffic.WorkerServices.Sms.Application.csproj", "CountwareTraffic.WorkerServices.Sms.Application/"]
COPY ["CountwareTraffic.WorkerServices.Sms.Data/CountwareTraffic.WorkerServices.Sms.Data.csproj", "CountwareTraffic.WorkerServices.Sms.Data/"]
COPY ["Sensormatic.Tool.Efcore/Sensormatic.Tool.Efcore.csproj", "Sensormatic.Tool.Efcore/"]
COPY ["Sensormatic.Tool.MongoDb/Sensormatic.Tool.MongoDb.csproj", "Sensormatic.Tool.MongoDb/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.Queue/Sensormatic.Tool.Queue.csproj", "Sensormatic.Tool.Queue/"]
COPY ["Sensormatic.Tool.QueueModel/Sensormatic.Tool.QueueModel.csproj", "Sensormatic.Tool.QueueModel/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
COPY ["Sensormatic.Tool.Sms/Sensormatic.Tool.Sms.csproj", "Sensormatic.Tool.Sms/"]
RUN dotnet restore "CountwareTraffic.WorkerServices.Sms.Consumer/CountwareTraffic.WorkerServices.Sms.Consumer.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.WorkerServices.Sms.Consumer"
RUN dotnet build "CountwareTraffic.WorkerServices.Sms.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.WorkerServices.Sms.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.WorkerServices.Sms.Consumer.dll"]