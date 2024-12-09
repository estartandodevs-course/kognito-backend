using Kognito.Usuarios.App.ViewModels;

namespace Kognito.Usuarios.App.Queries;

public interface IEmblemaQueries
{
    Task<IEnumerable<EmblemaViewModel>> ObterEmblemasDesbloqueados(Guid usuarioId);
    Task<EmblemaViewModel> ObterProximoEmblema(Guid usuarioId);
}
