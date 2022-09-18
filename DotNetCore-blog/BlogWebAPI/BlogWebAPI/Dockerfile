#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BlogWebAPI/ArticleService.WebAPI.csproj", "BlogWebAPI/"]
COPY ["BlogCommon/DomainCommon.csproj", "BlogCommon/"]
COPY ["CommonInfrastructure/CommonInfrastructure.csproj", "CommonInfrastructure/"]
COPY ["Article.Infrastructure/ArticleService.Infrastructure.csproj", "Article.Infrastructure/"]
COPY ["Aritcle.Domian/ArticleService.Domain.csproj", "Aritcle.Domian/"]
COPY ["Commons/CommonHelpers.csproj", "Commons/"]
COPY ["CommonInitializer/CommonInitializer.csproj", "CommonInitializer/"]
COPY ["CommomFilters/CommomFilters.csproj", "CommomFilters/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
COPY ["CommomConst/CommomConst.csproj", "CommomConst/"]
COPY ["CommonMiddleWare/CommonMiddleWare.csproj", "CommonMiddleWare/"]
COPY ["CommomJWT/CommomJWT.csproj", "CommomJWT/"]
COPY ["CommomInterface/CommomInterface.csproj", "CommomInterface/"]
RUN dotnet restore "BlogWebAPI/ArticleService.WebAPI.csproj"
COPY . .
WORKDIR "/src/BlogWebAPI"
RUN dotnet build "ArticleService.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArticleService.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArticleService.WebAPI.dll"]