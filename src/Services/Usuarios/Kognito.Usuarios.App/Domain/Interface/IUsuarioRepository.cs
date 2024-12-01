using EstartandoDevsCore.Data;

namespace Kognito.Usuarios.App.Domain.Interface;

public interface IUsuariosRepository : IRepository<Usuario>, IDisposable
{
    Task<IEnumerable<Usuario>> ObterTodosAsync();
    Task<Usuario> ObterPorEmail(string email);
    Task<IEnumerable<Emblemas>> ObterEmblemasAsync(Guid usuarioId);
    Task<IEnumerable<Metas>> ObterMetasAsync(Guid usuarioId);
    Task<Metas> ObterMetaPorId(Guid metaId);
    Task<Usuario> ObterPorCpf(string cpf);
    void Adicionar(Usuario usuario);
    void Atualizar(Usuario usuario);
    void AdicionarMeta(Metas meta);
    void AtualizarMeta(Metas meta);
    void RemoverMeta(Metas meta);
    void Apagar(Func<Usuario, bool> predicate);
}