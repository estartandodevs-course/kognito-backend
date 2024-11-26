using System;
using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.Domain.Interfaces;
public interface IEmblemaRepository : IRepository<Emblemas>
{
    Task<Emblemas> ObterPorId(Guid emblemaId);
    void Adicionar(Emblemas emblema);
    void Atualizar(Emblemas emblema);
    void Remover(Emblemas emblema);
}
