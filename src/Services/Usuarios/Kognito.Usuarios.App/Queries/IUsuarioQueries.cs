using Kognito.Usuarios.App.ViewModels;
using Kognito.Usuarios.App.Domain;

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
    Task<IEnumerable<MetaViewModel>> ObterMetasConcluidasHoje(Guid usuarioId);
    Task<bool> VerificarTipoUsuario(Guid usuarioId);

}
