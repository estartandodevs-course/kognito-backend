using System;
using System.Data.Common;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

public interface ITurmaRepository : IRepository<Turma>, IDisposable
{
    Task<IEnumerable<Turma>> ObterTurmas();

    Task<Turma> ObterPorId(Guid TurmaId);
    Task Adicionar(Turma turma);
    Task Atualizar(Turma turma);
    Task Remover(Turma turma);
    Task<DbConnection> ObterConexao();
}
