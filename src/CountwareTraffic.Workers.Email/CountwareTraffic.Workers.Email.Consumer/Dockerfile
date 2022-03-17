#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CountwareTraffic.Workers.Email/CountwareTraffic.Workers.Email.Consumer/CountwareTraffic.Workers.Email.Consumer.csproj", "src/CountwareTraffic.Workers.Email/CountwareTraffic.Workers.Email.Consumer/"]
COPY ["src/BuildingBlocks/Mhd.Framework.RabbitMq/Mhd.Framework.RabbitMq.csproj", "src/BuildingBlocks/Mhd.Framework.RabbitMq/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Ioc/Mhd.Framework.Ioc.csproj", "src/BuildingBlocks/Mhd.Framework.Ioc/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Common/Mhd.Framework.Common.csproj", "src/BuildingBlocks/Mhd.Framework.Common/"]
COPY ["src/CountwareTraffic.Workers.Email/CountwareTraffic.Workers.Email.Application/CountwareTraffic.Workers.Email.Application.csproj", "src/CountwareTraffic.Workers.Email/CountwareTraffic.Workers.Email.Application/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Email/Mhd.Framework.Email.csproj", "src/BuildingBlocks/Mhd.Framework.Email/"]
COPY ["src/CountwareTraffic.Workers.Email/CountwareTraffic.Workers.Email.Data/CountwareTraffic.Workers.Email.Data.csproj", "src/CountwareTraffic.Workers.Email/CountwareTraffic.Workers.Email.Data/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Efcore/Mhd.Framework.Efcore.csproj", "src/BuildingBlocks/Mhd.Framework.Efcore/"]
COPY ["src/BuildingBlocks/Mhd.Framework.MongoDb/Mhd.Framework.MongoDb.csproj", "src/BuildingBlocks/Mhd.Framework.MongoDb/"]
COPY ["src/BuildingBlocks/Mhd.Framework.Core/Mhd.Framework.Core.csproj", "src/BuildingBlocks/Mhd.Framework.Core/"]
COPY ["src/BuildingBlocks/Mhd.Framework.QueueModel/Mhd.Framework.QueueModel.csproj", "src/BuildingBlocks/Mhd.Framework.QueueModel/"]
RUN dotnet restore "src/CountwareTraffic.Workers.Email/CountwareTraffic.Workers.Email.Consumer/CountwareTraffic.Workers.Email.Consumer.csproj"
COPY . .
WORKDIR "/src/src/CountwareTraffic.Workers.Email/CountwareTraffic.Workers.Email.Consumer"
RUN dotnet build "CountwareTraffic.Workers.Email.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Workers.Email.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Workers.Email.Consumer.dll"]
