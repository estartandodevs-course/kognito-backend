using System;
using System.Data.Common;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

public interface IConteudoRepository : IRepository<Conteudo>, IDisposable
{
    Task<IEnumerable<Conteudo>> ObterTodosConteudo();
    void Adicionar(Conteudo conteudo);
    void Atualizar(Conteudo conteudo);
    void Remover (Conteudo conteudo);
    DbConnection ObterConexao();
}
