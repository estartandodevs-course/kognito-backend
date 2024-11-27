using System;
using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.App.Domain;
public interface IUsuariosRepository : IRepository<Usuarios>
{
    Task<App.Domain.Usuarios> ObterPorId(Guid usuarioId);
    void Adicionar(Usuarios usuario);
    void Atualizar(Usuarios usuario);
    void Remover(Usuarios usuario);
}