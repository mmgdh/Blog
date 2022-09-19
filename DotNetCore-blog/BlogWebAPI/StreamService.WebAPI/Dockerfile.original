#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StreamService.WebAPI/StreamService.WebAPI.csproj", "StreamService.WebAPI/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
COPY ["CommomConst/CommomConst.csproj", "CommomConst/"]
COPY ["StreamService.Domain/StreamService.Domain.csproj", "StreamService.Domain/"]
COPY ["BlogCommon/DomainCommon.csproj", "BlogCommon/"]
COPY ["CommonInitializer/CommonInitializer.csproj", "CommonInitializer/"]
COPY ["CommomFilters/CommomFilters.csproj", "CommomFilters/"]
COPY ["CommonInfrastructure/CommonInfrastructure.csproj", "CommonInfrastructure/"]
COPY ["Commons/CommonHelpers.csproj", "Commons/"]
COPY ["CommonMiddleWare/CommonMiddleWare.csproj", "CommonMiddleWare/"]
COPY ["CommomJWT/CommomJWT.csproj", "CommomJWT/"]
COPY ["CommomInterface/CommomInterface.csproj", "CommomInterface/"]
COPY ["StreamService.Infrastructure/StreamService.Infrastructure.csproj", "StreamService.Infrastructure/"]
COPY ["IdentityService.Domain/IdentityService.Domain.csproj", "IdentityService.Domain/"]
RUN dotnet restore "StreamService.WebAPI/StreamService.WebAPI.csproj"
COPY . .
WORKDIR "/src/StreamService.WebAPI"
RUN dotnet build "StreamService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StreamService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StreamService.WebAPI.dll"]