using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.App.Domain.Interface;

public interface IEmblemaRepository : IRepository<Emblemas>, IDisposable
{
    Task<IEnumerable<Emblemas>> ObterPorAlunoAsync(Guid alunoId);
    
    Task AdicionarAsync(Emblemas emblema);
}
