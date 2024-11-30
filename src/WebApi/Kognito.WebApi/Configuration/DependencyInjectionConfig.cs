using EstartandoDevsCore.Mediator;
using FluentValidation.Results;
using Kognito.Tarefas.App.Commands;
using Kognito.Tarefas.App.Queries;
using Kognito.Tarefas.Domain.interfaces;
using Kognito.Tarefas.Domain.Repositories;
using Kognito.Usuarios.App.Domain.Interface;
using Kognito.Usuarios.App.Infra.Repositories;
using MediatR;

namespace Kognito.WebApi.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<ITarefaQueries, TarefaQueries>();

        services.AddScoped<IUsuariosRepository, UsuarioRepository>();

        services.AddScoped<IRequestHandler<CriarTarefaCommand, ValidationResult>, TarefasCommandHandler>();
        services.AddScoped<IRequestHandler<AtualizarTarefaCommand, ValidationResult>, TarefasCommandHandler>();
        services.AddScoped<IRequestHandler<EntregarTarefaCommand, ValidationResult>, TarefasCommandHandler>();
    }
}