using System;
using Kognito.Turmas.App.ViewModels;
using Kognito.Turmas.Domain;
namespace Kognito.Turmas.App.Queries;

public interface IConteudoQueries
{
    /// <summary>
    /// Obtém um conteúdo pelo seu Id
    /// </summary>
    Task<IEnumerable<ConteudoViewModel>> ObterPorId(Guid id);
    
    /// <summary>
    /// Obtém todos os conteúdos cadastrados
    /// </summary>
    Task<IEnumerable<ConteudoViewModel>> ObterTodosConteudos();
}