#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["Mobile.BFF.CountwareTraffic.HttpAggregator/Mobile.BFF.CountwareTraffic.HttpAggregator.csproj", "Mobile.BFF.CountwareTraffic.HttpAggregator/"]
COPY ["Sensormatic.Tool.Api/Sensormatic.Tool.Api.csproj", "Sensormatic.Tool.Api/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
COPY ["Sensormatic.Tool.Grpc/Sensormatic.Tool.Grpc.csproj", "Sensormatic.Tool.Grpc/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
RUN dotnet restore "Mobile.BFF.CountwareTraffic.HttpAggregator/Mobile.BFF.CountwareTraffic.HttpAggregator.csproj"
COPY . .
WORKDIR "/src/Mobile.BFF.CountwareTraffic.HttpAggregator"
RUN dotnet build "Mobile.BFF.CountwareTraffic.HttpAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mobile.BFF.CountwareTraffic.HttpAggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mobile.BFF.CountwareTraffic.HttpAggregator.dll"]