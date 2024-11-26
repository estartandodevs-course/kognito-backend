using System;
using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.Domain.interfaces;
public interface IUsuariosRepository : IRepository<Usuarios>
{
    Task<Usuarios> ObterPorId(Guid usuarioId);
    void Adicionar(Usuarios usuario);
    void Atualizar(Usuarios usuario);
    void Remover(Usuarios usuario);
}