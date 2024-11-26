using System;
using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.Domain.Interfaces;
public interface IMetaRepository : IRepository<Metas>
{
    Task<Metas> ObterPorId(Guid metaId);
    void Adicionar(Metas meta);
    void Atualizar(Metas meta);
    void Remover(Metas meta);
}