using Kognito.Turmas.App.ViewModels;
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

    /// <summary>
    /// Obtém todos os conteúdos de uma turma específica
    /// </summary>
    Task<IEnumerable<ConteudoViewModel>> ObterPorTurma(Guid turmaId);

    /// <summary>
    /// Obtém a quantidade de conteúdos de uma turma
    /// </summary>
    Task<int> ObterQuantidadeConteudosPorTurma(Guid turmaId);
}