FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/WebApi/Kognito.WebApi/Kognito.WebApi.csproj", "src/WebApi/Kognito.WebApi/"]
RUN dotnet restore "src/WebApi/Kognito.WebApi/Kognito.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi/Kognito.WebApi"
RUN dotnet build "Kognito.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kognito.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kognito.WebApi.dll"]
