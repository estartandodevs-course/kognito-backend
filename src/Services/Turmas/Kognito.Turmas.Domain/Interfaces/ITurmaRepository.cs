using System;
using System.Data.Common;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

public interface ITurmaRepository : IRepository<Turma>, IDisposable
{
     Task<IEnumerable<Turma>> ObterTurmasPorProfessor(Guid professorId);
    Task<IEnumerable<Turma>> ObterTodos();
    Task<DbConnection> ObterConexao();
    Task<int> ObterQuantidadeAlunos(Guid turmaId);
}
