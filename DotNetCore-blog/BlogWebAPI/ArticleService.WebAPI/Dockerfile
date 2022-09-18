#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ArticleService.WebAPI/ArticleService.WebAPI.csproj", "ArticleService.WebAPI/"]
COPY ["BlogCommon/DomainCommon.csproj", "BlogCommon/"]
COPY ["ArticleService.Infrastructure/ArticleService.Infrastructure.csproj", "ArticleService.Infrastructure/"]
COPY ["AritcleService.Domian/ArticleService.Domain.csproj", "AritcleService.Domian/"]
COPY ["CommonInfrastructure/CommonInfrastructure.csproj", "CommonInfrastructure/"]
COPY ["Commons/CommonHelpers.csproj", "Commons/"]
COPY ["CommonInitializer/CommonInitializer.csproj", "CommonInitializer/"]
COPY ["CommomFilters/CommomFilters.csproj", "CommomFilters/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
COPY ["CommomConst/CommomConst.csproj", "CommomConst/"]
COPY ["CommonMiddleWare/CommonMiddleWare.csproj", "CommonMiddleWare/"]
COPY ["CommomJWT/CommomJWT.csproj", "CommomJWT/"]
COPY ["CommomInterface/CommomInterface.csproj", "CommomInterface/"]
RUN dotnet restore "ArticleService.WebAPI/ArticleService.WebAPI.csproj"
COPY . .
WORKDIR "/src/ArticleService.WebAPI"
RUN dotnet build "ArticleService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArticleService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleService.WebAPI.dll"]