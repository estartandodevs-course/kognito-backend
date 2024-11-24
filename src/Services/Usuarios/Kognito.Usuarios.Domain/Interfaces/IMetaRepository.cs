using System;
using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.Domain.Interfaces
public interface IMetaRepository : IRepository<Meta>
{
    Task<Meta> ObterPorId(Guid metaId);
    void Adicionar(Meta meta);
    void Atualizar(Meta meta);
    void Remover(Meta meta);
}
