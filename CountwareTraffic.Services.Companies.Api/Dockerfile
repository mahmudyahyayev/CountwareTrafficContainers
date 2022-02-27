#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.Services.Companies.Api/CountwareTraffic.Services.Companies.Api.csproj", "CountwareTraffic.Services.Companies.Api/"]
COPY ["Sensormatic.Tool.Api/Sensormatic.Tool.Api.csproj", "Sensormatic.Tool.Api/"]
COPY ["Sensormatic.Tool.Core/Sensormatic.Tool.Core.csproj", "Sensormatic.Tool.Core/"]
COPY ["Sensormatic.Tool.Ioc/Sensormatic.Tool.Ioc.csproj", "Sensormatic.Tool.Ioc/"]
COPY ["Sensormatic.Tool.Grpc/Sensormatic.Tool.Grpc.csproj", "Sensormatic.Tool.Grpc/"]
COPY ["CountwareTraffic.Services.Companies.Infrastructure/CountwareTraffic.Services.Companies.Infrastructure.csproj", "CountwareTraffic.Services.Companies.Infrastructure/"]
COPY ["CountwareTraffic.Services.Companies.Application/CountwareTraffic.Services.Companies.Application.csproj", "CountwareTraffic.Services.Companies.Application/"]
COPY ["Sensormatic.Tool.QueueModel/Sensormatic.Tool.QueueModel.csproj", "Sensormatic.Tool.QueueModel/"]
COPY ["Sensormatic.Tool.Queue/Sensormatic.Tool.Queue.csproj", "Sensormatic.Tool.Queue/"]
COPY ["Sensormatic.Tool.Common/Sensormatic.Tool.Common.csproj", "Sensormatic.Tool.Common/"]
COPY ["CountwareTraffic.Services.Companies.Core/CountwareTraffic.Services.Companies.Core.csproj", "CountwareTraffic.Services.Companies.Core/"]
COPY ["Sensormatic.Tool.Efcore/Sensormatic.Tool.Efcore.csproj", "Sensormatic.Tool.Efcore/"]
COPY ["AuditLog/AuditLog.csproj", "AuditLog/"]
RUN dotnet restore "CountwareTraffic.Services.Companies.Api/CountwareTraffic.Services.Companies.Api.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.Services.Companies.Api"
RUN dotnet build "CountwareTraffic.Services.Companies.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Companies.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Companies.Api.dll"]