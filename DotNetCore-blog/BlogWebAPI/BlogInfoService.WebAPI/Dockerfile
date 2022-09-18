#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlogInfoService.WebAPI/BlogInfoService.WebAPI.csproj", "BlogInfoService.WebAPI/"]
COPY ["BlogInfoService.Domain/BlogInfoService.Domain.csproj", "BlogInfoService.Domain/"]
COPY ["BlogCommon/DomainCommon.csproj", "BlogCommon/"]
COPY ["CommomInterface/CommomInterface.csproj", "CommomInterface/"]
COPY ["BlogInfoService.Infrastructure/BlogInfoService.Infrastructure.csproj", "BlogInfoService.Infrastructure/"]
COPY ["CommonInitializer/CommonInitializer.csproj", "CommonInitializer/"]
COPY ["CommomFilters/CommomFilters.csproj", "CommomFilters/"]
COPY ["CommonInfrastructure/CommonInfrastructure.csproj", "CommonInfrastructure/"]
COPY ["Commons/CommonHelpers.csproj", "Commons/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
COPY ["CommomConst/CommomConst.csproj", "CommomConst/"]
COPY ["CommonMiddleWare/CommonMiddleWare.csproj", "CommonMiddleWare/"]
COPY ["CommomJWT/CommomJWT.csproj", "CommomJWT/"]
RUN dotnet restore "BlogInfoService.WebAPI/BlogInfoService.WebAPI.csproj"
COPY . .
WORKDIR "/src/BlogInfoService.WebAPI"
RUN dotnet build "BlogInfoService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BlogInfoService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogInfoService.WebAPI.dll"]