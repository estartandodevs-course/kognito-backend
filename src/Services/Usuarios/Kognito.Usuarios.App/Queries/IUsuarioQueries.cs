using Kognito.Usuarios.App.ViewModels;

namespace Kognito.Usuarios.App.Queries;

public interface IUsuarioQueries
{
    Task<UsuarioViewModel> ObterPorId(Guid id);
    Task<IEnumerable<EmblemaViewModel>> ObterEmblemas(Guid usuarioId);
    Task<IEnumerable<MetaViewModel>> ObterMetas(Guid usuarioId);
    Task<UsuarioViewModel> ObterPorEmail(string email);
    Task<OfensivaViewModel> ObterOfensiva(Guid usuarioId);
    Task<string> ObterCodigoRecuperacaoPorEmail(string email);
    Task<Guid?> ObterCodigoPaiPorEmail(string email);
    Task<string> ObterResponsavelEmailPorId(Guid id);
}
