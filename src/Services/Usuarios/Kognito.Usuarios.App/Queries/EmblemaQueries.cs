using Kognito.Usuarios.App.Domain.Interface;
using Kognito.Usuarios.App.ViewModels;

namespace Kognito.Usuarios.App.Queries;

public class EmblemaQueries : IEmblemaQueries
{
    private readonly IEmblemaRepository _emblemaRepository;

    public EmblemaQueries(IEmblemaRepository emblemaRepository)
    {
        _emblemaRepository = emblemaRepository;
    }
    
    public async Task<IEnumerable<EmblemaViewModel>> ObterEmblemasDesbloqueados(
        Guid usuarioId)
    {
        var emblemas = await _emblemaRepository.ObterEmblemasUsuario(usuarioId);
        return emblemas.Where(e => e.Desbloqueado)
            .Select(e => new EmblemaViewModel(e));
    }

    public async Task<EmblemaViewModel> ObterProximoEmblema(Guid usuarioId)
    {
        var proximoEmblema = await _emblemaRepository.ObterProximoEmblemaDisponivel(usuarioId);
        
        return proximoEmblema != null 
            ? new EmblemaViewModel(proximoEmblema)
            : null;
    }
}