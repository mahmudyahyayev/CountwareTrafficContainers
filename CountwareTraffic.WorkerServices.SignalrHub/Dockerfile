#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.WorkerServices.SignalrHub/CountwareTraffic.WorkerServices.SignalrHub.csproj", "CountwareTraffic.WorkerServices.SignalrHub/"]
COPY ["CountwareTraffic.WorkerServices.SignalrHub.Application/CountwareTraffic.WorkerServices.SignalrHub.Application.csproj", "CountwareTraffic.WorkerServices.SignalrHub.Application/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.QueueModel/Sensormatic.Tool.QueueModel.csproj", "Sensormatic.Tool.QueueModel/"]
COPY ["Sensormatic.Tool.Queue/Sensormatic.Tool.Queue.csproj", "Sensormatic.Tool.Queue/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
RUN dotnet restore "CountwareTraffic.WorkerServices.SignalrHub/CountwareTraffic.WorkerServices.SignalrHub.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.WorkerServices.SignalrHub"
RUN dotnet build "CountwareTraffic.WorkerServices.SignalrHub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.WorkerServices.SignalrHub.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.WorkerServices.SignalrHub.dll"]
