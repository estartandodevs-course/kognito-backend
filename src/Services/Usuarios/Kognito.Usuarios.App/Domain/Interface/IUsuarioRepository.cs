using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.App.Domain.Interface;

public interface IUsuariosRepository : IRepository<Usuario>, IDisposable
{
    Task<IEnumerable<Usuario>> ObterTodosAsync();
    Task<IEnumerable<Emblemas>> ObterEmblemasAsync(Guid usuarioId);
    Task<IEnumerable<Metas>> ObterMetasAsync(Guid usuarioId);
    void Adicionar(Usuario usuario);
    void Atualizar(Usuario usuario);
    void Apagar(Func<Usuario, bool> predicate);
}