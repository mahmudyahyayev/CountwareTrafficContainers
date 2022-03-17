#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CountwareTraffic.Services.Identity/CountwareTraffic.Services.Identity.Grpc/CountwareTraffic.Services.Identity.Grpc.csproj", "src/CountwareTraffic.Services.Identity/CountwareTraffic.Services.Identity.Grpc/"]
COPY ["src/CountwareTraffic.Services.Identity/CountwareTraffic.Services.Identity.Application/CountwareTraffic.Services.Identity.Application.csproj", "src/CountwareTraffic.Services.Identity/CountwareTraffic.Services.Identity.Application/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Ioc/Mhd.Framework.Ioc.csproj", "src/BuildingBlocks/Mhd.Framework.Ioc/"]
COPY ["src/CrossCuttingConcerns/Observability/Observability.csproj", "src/CrossCuttingConcerns/Observability/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Core/Mhd.Framework.Core.csproj", "src/BuildingBlocks/Mhd.Framework.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Grpc/Mhd.Framework.Grpc.csproj", "src/BuildingBlocks/Mhd.Framework.Grpc/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Common/Mhd.Framework.Common.csproj", "src/BuildingBlocks/Mhd.Framework.Common/"]
RUN dotnet restore "src/CountwareTraffic.Services.Identity/CountwareTraffic.Services.Identity.Grpc/CountwareTraffic.Services.Identity.Grpc.csproj"
COPY . .
WORKDIR "/src/src/CountwareTraffic.Services.Identity/CountwareTraffic.Services.Identity.Grpc"
RUN dotnet build "CountwareTraffic.Services.Identity.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Services.Identity.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Services.Identity.Grpc.dll"]
