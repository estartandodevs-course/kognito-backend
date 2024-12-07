using EstartandoDevsWebApiCore.Identidade;
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

        services.AddDbContext<TarefasContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));   

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
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerConfiguration();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(PermissoesDeOrigem);
        
        app.MapControllers();
        app.UseAuthConfiguration();
    }
}