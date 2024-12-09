using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.App.Domain.Interface
{
    public interface IEmblemaRepository : IRepository<Emblemas>
    {
        Task AdicionarAsync(Emblemas emblema);
    
        Task<IEnumerable<Emblemas>> ObterEmblemasUsuario(Guid usuarioId);
        Task<int> ObterQuantidadeEntregasUsuario(Guid usuarioId);
        Task<Emblemas> ObterProximoEmblemaDisponivel(Guid usuarioId);
    }
}
