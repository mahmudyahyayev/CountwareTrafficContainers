#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Events.ElasticRepair/CountwareTraffic.Services.Events.ElasticRepair.csproj", "src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Events.ElasticRepair/"]
RUN dotnet restore "src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Events.ElasticRepair/CountwareTraffic.Services.Events.ElasticRepair.csproj"
COPY . .
WORKDIR "/src/src/CountwareTraffic.Services.Event/CountwareTraffic.Services.Events.ElasticRepair"
RUN dotnet build "CountwareTraffic.Services.Events.ElasticRepair.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Events.ElasticRepair.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Events.ElasticRepair.dll"]
