using System;
using System.Data.Common;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

public interface ITurmaRepository : IRepository<Turma>, IDisposable
{
    Task<IEnumerable<Turma>> ObterTurmasPorProfessor(Guid professorId);
    Task<DbConnection> ObterConexao();
    Task<int> ObterQuantidadeAlunos(Guid turmaId);
    Task<Turma> ObterPorHashAcesso(string hash);
    Task<IEnumerable<Turma>> ObterTurmasPorAluno(Guid alunoId);
}
