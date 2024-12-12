using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.App.Domain.Interface;

public interface IEmblemaRepository : IRepository<Emblemas>
{
    Task<IEnumerable<Emblemas>> ObterEmblemasUsuario(Guid usuarioId);
    Task<int> ObterQuantidadeEntregasUsuario(Guid usuarioId);
    Task<Emblemas> ObterProximoEmblemaDisponivel(Guid usuarioId);
    Task AdicionarAsync(Emblemas emblema);
}
