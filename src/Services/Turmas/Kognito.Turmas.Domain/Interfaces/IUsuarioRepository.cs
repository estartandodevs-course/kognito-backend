using System;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>, IDisposable
{
    Task<Usuario> ObterPorId(Guid usuario);
    void Adicionar(Usuario usuario);
    void Atualizar(Usuario usuario);
    void Remover(Usuario usuario);
}

