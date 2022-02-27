#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.WorkerServices.Email.Consumer/CountwareTraffic.WorkerServices.Email.Consumer.csproj", "CountwareTraffic.WorkerServices.Email.Consumer/"]
COPY ["CountwareTraffic.WorkerServices.Email.Application/CountwareTraffic.WorkerServices.Email.Application.csproj", "CountwareTraffic.WorkerServices.Email.Application/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.Email/Sensormatic.Tool.Email.csproj", "Sensormatic.Tool.Email/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
COPY ["CountwareTraffic.WorkerServices.Email.Data/CountwareTraffic.WorkerServices.Email.Data.csproj", "CountwareTraffic.WorkerServices.Email.Data/"]
COPY ["Sensormatic.Tool.Efcore/Sensormatic.Tool.Efcore.csproj", "Sensormatic.Tool.Efcore/"]
COPY ["Sensormatic.Tool.MongoDb/Sensormatic.Tool.MongoDb.csproj", "Sensormatic.Tool.MongoDb/"]
COPY ["Sensormatic.Tool.QueueModel/Sensormatic.Tool.QueueModel.csproj", "Sensormatic.Tool.QueueModel/"]
COPY ["Sensormatic.Tool.Queue/Sensormatic.Tool.Queue.csproj", "Sensormatic.Tool.Queue/"]
RUN dotnet restore "CountwareTraffic.WorkerServices.Email.Consumer/CountwareTraffic.WorkerServices.Email.Consumer.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.WorkerServices.Email.Consumer"
RUN dotnet build "CountwareTraffic.WorkerServices.Email.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.WorkerServices.Email.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.WorkerServices.Email.Consumer.dll"]
