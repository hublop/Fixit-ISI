FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as base
ENV ASPNETCORE_ENVIRONMENT Development
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY Fixit.sln ./
COPY Fixit.Application/*.csproj ./Fixit.Application/
COPY Fixit.Domain/*.csproj ./Fixit.Domain/
COPY Fixit.EventBus/*.csproj ./Fixit.EventBus/
COPY Fixit.Infrastructure/*.csproj ./Fixit.Infrastructure/
COPY Fixit.Persistance/*.csproj ./Fixit.Persistance/
COPY Fixit.Shared/*.csproj ./Fixit.Shared/
COPY Fixit.WebApi/*.csproj ./Fixit.WebApi/


RUN dotnet restore
COPY . .
WORKDIR /src/Fixit.WebApi
RUN dotnet build -c Development -o /app

FROM build AS publish
RUN dotnet publish -c Development -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Fixit.WebApi.dll"]