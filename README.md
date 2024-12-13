# Como rodar o projeto

## Pré-requisitos

1. Baixe e instale o [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0).
2. Baixe e instale o [Docker](https://www.docker.com/get-started).

## Passos para Configuração

1. Abra o terminal e navegue até a pasta do projeto WebApi:
   ```bash
   cd src/WebApi/Kognito.WebApi
   ```

2. Suba os containers do Docker:
   ```bash
   docker-compose up -d
   ```

3. Execute todas as migrações do banco de dados:

    - **Migrações de Usuários**
       ```bash
       dotnet ef database update --context UsuarioContext
       ```
    - **Migrações de Turmas**
       ```bash
       dotnet ef database update --context TurmaContext
       ```
    - **Migrações de Tarefas**
       ```bash
       dotnet ef database update --context TarefasContext
       ```
    - **Migrações de Autenticação**
       ```bash
       dotnet ef database update --context AutenticacaoDbContext
       ```

4. Execute o projeto:
   ```bash
   dotnet run
   ```

## Acesso à API

- **Swagger**: [http://localhost:5225/swagger](http://localhost:5225/swagger)