using System;
using System.Data.Common;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

public interface ITurmaRepository : IRepository<Turma>, IDisposable
{
    Task<IEnumerable<Turma>> ObterTurmas();

    // Task<Turma> ObterPorId(Guid TurmaId);
    void Adicionar(Turma turma);
    void Atualizar(Turma turma);
    void Remover(Turma turma);
    DbConnection ObterConexao();
}
