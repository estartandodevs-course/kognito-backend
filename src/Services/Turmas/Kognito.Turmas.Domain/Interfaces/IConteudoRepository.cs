using System;
using System.Data.Common;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

public interface IConteudoRepository : IRepository<Conteudo>, IDisposable
{
    Task<IEnumerable<Conteudo>> ObterTodosConteudo();
    Task Adicionar(Conteudo conteudo);
    Task Atualizar(Conteudo conteudo);
    Task Remover(Conteudo conteudo);
    Task<DbConnection> ObterConexao();

}