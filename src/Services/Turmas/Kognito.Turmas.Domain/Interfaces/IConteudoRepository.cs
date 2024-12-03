using System;
using System.Data.Common;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

public interface IConteudoRepository : IRepository<Conteudo>, IDisposable
{
    Task<IEnumerable<Conteudo>> ObterTodosConteudo();
    Task<IEnumerable<Conteudo>> ObterPorTurma(Guid turmaId);
    Task<int> ObterQuantidadeConteudosPorTurma(Guid turmaId);
    Task<DbConnection> ObterConexao();
    Task<Conteudo> ObterPorId(Guid id);
}