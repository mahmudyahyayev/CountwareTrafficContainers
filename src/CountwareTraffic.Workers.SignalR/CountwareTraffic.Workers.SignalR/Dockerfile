#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CountwareTraffic.Workers.SignalR/CountwareTraffic.Workers.SignalR/CountwareTraffic.Workers.SignalR.csproj", "src/CountwareTraffic.Workers.SignalR/CountwareTraffic.Workers.SignalR/"]
COPY ["src/CountwareTraffic.Workers.SignalR/CountwareTraffic.Workers.SignalR.Application/CountwareTraffic.Workers.SignalR.Application.csproj", "src/CountwareTraffic.Workers.SignalR/CountwareTraffic.Workers.SignalR.Application/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Ioc/Mhd.Framework.Ioc.csproj", "src/BuildingBlocks/Mhd.Framework.Ioc/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Core/Mhd.Framework.Core.csproj", "src/BuildingBlocks/Mhd.Framework.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Common/Mhd.Framework.Common.csproj", "src/BuildingBlocks/Mhd.Framework.Common/"]
COPY ["src/BuildingBlocks/Mhd.Framework.RabbitMq/Mhd.Framework.RabbitMq.csproj", "src/BuildingBlocks/Mhd.Framework.RabbitMq/"]
COPY ["src/BuildingBlocks/Mhd.Framework.QueueModel/Mhd.Framework.QueueModel.csproj", "src/BuildingBlocks/Mhd.Framework.QueueModel/"]
RUN dotnet restore "src/CountwareTraffic.Workers.SignalR/CountwareTraffic.Workers.SignalR/CountwareTraffic.Workers.SignalR.csproj"
COPY . .
WORKDIR "/src/src/CountwareTraffic.Workers.SignalR/CountwareTraffic.Workers.SignalR"
RUN dotnet build "CountwareTraffic.Workers.SignalR.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Workers.SignalR.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Workers.SignalR.dll"]
