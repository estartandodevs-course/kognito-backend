using System;
using System.Data.Common;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

/// <summary>
/// Interface responsável pelo repositório de Turmas
/// </summary>
public interface ITurmaRepository : IRepository<Turma>, IDisposable
{
<<<<<<< HEAD
    Task<IEnumerable<Turma>> ObterTurmas();

    Task<Turma> ObterPorId(Guid TurmaId);
    Task Adicionar(Turma turma);
    Task Atualizar(Turma turma);
    Task Remover(Turma turma);
    Task<DbConnection> ObterConexao();
}
=======
    /// <summary>
    /// Obtém todas as turmas de um professor específico
    /// </summary>
    /// <param name="professorId">ID do professor</param>
    /// <returns>Lista de turmas do professor</returns>
    Task<IEnumerable<Turma>> ObterTurmasPorProfessor(Guid professorId);

    /// <summary>
    /// Obtém todas as turmas cadastradas
    /// </summary>
    Task<IEnumerable<Turma>> ObterTodos();

    /// <summary>
    /// Obtém a conexão com o banco de dados
    /// </summary>
    Task<DbConnection> ObterConexao();

    /// <summary>
    /// Obtém a quantidade de alunos em uma turma
    /// </summary>
    Task<int> ObterQuantidadeAlunos(Guid turmaId);

    /// <summary>
    /// Obtém uma turma completa com todos os seus relacionamentos
    /// </summary>
    Task<Turma> ObterTurmaCompletaPorId(Guid id);

    /// <summary>
    /// Obtém um link de acesso pelo seu código
    /// </summary>
    Task<LinkDeAcesso> ObterLinkPorCodigo(Guid turmaId, string codigo);

    /// <summary>
    /// Obtém todos os links ativos de uma turma
    /// </summary>
    Task<IEnumerable<LinkDeAcesso>> ObterLinksAtivos(Guid turmaId);
}
>>>>>>> 892a573 (feat(turmas): implementa sistema de links de acesso para turmas)
