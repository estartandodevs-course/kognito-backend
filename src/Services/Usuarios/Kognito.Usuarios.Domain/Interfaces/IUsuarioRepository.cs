using System;
using EstartandoDevsCore.Data;

namespace Kognito.Usuario.Domain.interfaces;
public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario> ObterPorId(Guid usuarioId);
    void Adicionar(Usuario usuario);
    void Atualizar(Usuario usuario);
    void Remover(Usuario usuario);
}