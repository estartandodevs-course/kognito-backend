using System;
using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.Domain.Interfaces
public interface IEmblemaRepository : IRepository<Emblema>
{
    Task<Emblema> ObterPorId(Guid emblemaId);
    void Adicionar(Emblema emblema);
    void Atualizar(Emblema emblema);
    void Remover(Emblema emblema);
}
