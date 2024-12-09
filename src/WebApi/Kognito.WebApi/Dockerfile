FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./WebApi/Kognito.WebApi/Kognito.WebApi.csproj", "WebApi/Kognito.WebApi/"]
COPY ["./Services/Autenticacao/Kognito.Autenticacao.App/Kognito.Autenticacao.App.csproj", "Services/Autenticacao/Kognito.Autenticacao.App/Kognito.Autenticacao.App.csproj"]

COPY ["./Services/Tarefas/Kognito.Tarefas.App/Kognito.Tarefas.App.csproj", "Services/Tarefas/Kognito.Tarefas.App/Kognito.Tarefas.App.csproj"]
COPY ["./Services/Tarefas/Kognito.Tarefas.Domain/Kognito.Tarefas.Domain.csproj", "Services/Tarefas/Kognito.Tarefas.Domain/Kognito.Tarefas.Domain.csproj"]
COPY ["./Services/Tarefas/Kognito.Tarefas.App/Kognito.Tarefas.Infra.csproj", "Services/Tarefas/Kognito.Tarefas.Infra/Kognito.Tarefas.Infra.csproj"]

COPY ["./Services/Turmas/Kognito.Turmas.App/Kognito.Turmas.App.csproj", "Services/Turmas/Kognito.Turmas.App/Kognito.Turmas.App.csproj"]
COPY ["./Services/Turmas/Kognito.Turmas.Domain/Kognito.Turmas.Domain.csproj", "Services/Turmas/Kognito.Turmas.Domain/Kognito.Turmas.Domain.csproj"]
COPY ["./Services/Turmas/Kognito.Turmas.App/Kognito.Turmas.Infra.csproj", "Services/Turmas/Kognito.Turmas.Infra/Kognito.Turmas.Infra.csproj"]

COPY ["./Services/Usuarios/Kognito.Usuarios.App/Kognito.Usuarios.App.csproj", "Services/Usuarios/Kognito.Usuarios.App/Kognito.Usuarios.App.csproj"]

RUN dotnet restore "WebApi/Kognito.WebApi/Kognito.WebApi.csproj"
COPY . .
WORKDIR "WebApi/Kognito.WebApi"
RUN dotnet build "Kognito.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kognito.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kognito.WebApi.dll"]
