using EstartandoDevsCore.Mediator;
using FluentValidation.Results;
using Kognito.Tarefas.App.Commands;
using Kognito.Tarefas.App.Queries;
using Kognito.Tarefas.Domain.interfaces;
using Kognito.Tarefas.Domain.Repositories;
using Kognito.Turmas.App.Commands;
using Kognito.Turmas.App.Queries;
using Kognito.Turmas.Domain.Interfaces;
using Kognito.Usuarios.App.Commands;
using Kognito.Usuarios.App.Domain.Interface;
using Kognito.Usuarios.App.Infra.Repositories;
using Kognito.Usuarios.App.Queries;
using MediatR;

namespace Kognito.WebApi.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {

        services.AddScoped<IMediatorHandler, MediatorHandler>();
        

        services.AddScoped<IUsuariosRepository, UsuarioRepository>();
        services.AddScoped<IUsuarioQueries, UsuarioQueries>();
        services.AddMediatR(typeof(CriarUsuarioCommand));


        services.AddScoped<ITarefaRepository, TarefaRepository>();
        services.AddScoped<ITarefaQueries, TarefaQueries>();
        services.AddScoped<IRequestHandler<CriarTarefaCommand, ValidationResult>, TarefasCommandHandler>();
        services.AddScoped<IRequestHandler<AtualizarTarefaCommand, ValidationResult>, TarefasCommandHandler>();
        services.AddScoped<IRequestHandler<EntregarTarefaCommand, ValidationResult>, TarefasCommandHandler>();


        services.AddScoped<ITurmaQueries, TurmaQueries>();
        services.AddScoped<ITurmaRepository, TurmaRepository>();
        services.AddScoped<IRequestHandler<CriarTurmaCommand, ValidationResult>, TurmaCommandHandler>();
        services.AddScoped<IRequestHandler<AtualizarTurmaCommand, ValidationResult>, TurmaCommandHandler>();
        services.AddScoped<IRequestHandler<ExcluirTurmaCommand, ValidationResult>, TurmaCommandHandler>();
        services.AddScoped<IRequestHandler<CriarAlunoNaTurmaCommand, ValidationResult>, TurmaCommandHandler>();

        services.AddScoped<IConteudoRepository, ConteudoRepository>();
        services.AddScoped<IConteudoQueries, ConteudoQueries>();
        services.AddScoped<IRequestHandler<CriarConteudoCommand, ValidationResult>, ConteudoCommandHandler>();
        services.AddScoped<IRequestHandler<AtualizarConteudoCommand, ValidationResult>, ConteudoCommandHandler>();
        services.AddScoped<IRequestHandler<ExcluirConteudoCommand, ValidationResult>, ConteudoCommandHandler>();
        services.AddScoped<IRequestHandler<VincularConteudoTurmaCommand, ValidationResult>, ConteudoCommandHandler>();
    }
}