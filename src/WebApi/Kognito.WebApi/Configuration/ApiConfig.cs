using EstartandoDevsWebApiCore.Identidade;
using Kognito.Autenticacao.App.Data;
using Kognito.Tarefas.Infra.Data;
using Kognito.Turmas.Infra.Data;
using Kognito.Usuarios.App.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kognito.WebApi.Configuration;

public static class ApiConfig
{
    private const string ConexaoBancoDeDados = "KognitoConnection";
    private const string PermissoesDeOrigem = "_permissoesDeOrigem";

    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddDbContext<TarefasContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(ConexaoBancoDeDados)));

        services.AddDbContext<UsuarioContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(ConexaoBancoDeDados)));

        services.AddDbContext<TurmaContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(ConexaoBancoDeDados)));

        services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

        services.AddCors(options =>
        {
            options.AddPolicy(PermissoesDeOrigem,
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        app.UseSwaggerConfiguration();
        app.UseSwagger();
        app.UseSwaggerUI();
        
        using var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope();

        var contextTarefas = serviceScope.ServiceProvider.GetRequiredService<TarefasContext>();
        contextTarefas.Database.Migrate();

        var contextUsuarios = serviceScope.ServiceProvider.GetRequiredService<UsuarioContext>();
        contextUsuarios.Database.Migrate();

        var contextTurmas = serviceScope.ServiceProvider.GetRequiredService<TurmaContext>();
        contextTurmas.Database.Migrate();

        var contextAutenticacao = serviceScope.ServiceProvider.GetRequiredService<AutenticacaoDbContext>();
        contextAutenticacao.Database.Migrate();

        app.UseHttpsRedirection();
        app.UseCors(PermissoesDeOrigem);

        app.MapControllers();
        app.UseAuthConfiguration();
    }
}