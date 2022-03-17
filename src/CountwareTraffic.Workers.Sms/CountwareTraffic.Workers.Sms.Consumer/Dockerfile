#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["CountwareTraffic.Workers.Sms.Consumer/CountwareTraffic.Workers.Sms.Consumer.csproj", "CountwareTraffic.Workers.Sms.Consumer/"]
COPY ["CountwareTraffic.Workers.Sms.Application/CountwareTraffic.Workers.Sms.Application.csproj", "CountwareTraffic.Workers.Sms.Application/"]
COPY ["CountwareTraffic.Workers.Sms.Data/CountwareTraffic.Workers.Sms.Data.csproj", "CountwareTraffic.Workers.Sms.Data/"]
COPY ["Mhd.Framewrok.Efcore/Mhd.Framewrok.Efcore.csproj", "Mhd.Framewrok.Efcore/"]
COPY ["Mhd.Framewrok.MongoDb/Mhd.Framewrok.MongoDb.csproj", "Mhd.Framewrok.MongoDb/"]
COPY ["Mhd.Framewrok.Ioc/Mhd.Framewrok.Ioc.csproj", "Mhd.Framewrok.Ioc/"]
COPY ["Mhd.Framewrok.Core/Mhd.Framewrok.Core.csproj", "Mhd.Framewrok.Core/"]
COPY ["Mhd.Framewrok.Queue/Mhd.Framewrok.Queue.csproj", "Mhd.Framewrok.Queue/"]
COPY ["Mhd.Framewrok.QueueModel/Mhd.Framewrok.QueueModel.csproj", "Mhd.Framewrok.QueueModel/"]
COPY ["Mhd.Framewrok.Common/Mhd.Framewrok.Common.csproj", "Mhd.Framewrok.Common/"]
COPY ["Mhd.Framewrok.Sms/Mhd.Framewrok.Sms.csproj", "Mhd.Framewrok.Sms/"]
RUN dotnet restore "CountwareTraffic.Workers.Sms.Consumer/CountwareTraffic.Workers.Sms.Consumer.csproj"
COPY . .
WORKDIR "/src/CountwareTraffic.Workers.Sms.Consumer"
RUN dotnet build "CountwareTraffic.Workers.Sms.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CountwareTraffic.Workers.Sms.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CountwareTraffic.Workers.Sms.Consumer.dll"]