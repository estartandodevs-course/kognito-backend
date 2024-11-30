using Kognito.Usuarios.App.ViewModels;
using Kognito.Usuarios.App.Domain.Interface;

namespace Kognito.Usuarios.App.Queries;

public class UsuarioQueries : IUsuarioQueries
{
    private readonly IUsuariosRepository _usuarioRepository;

    public UsuarioQueries(IUsuariosRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioViewModel> ObterPorId(Guid id)
    {
        var usuario = await _usuarioRepository.ObterPorId(id);
        return UsuarioViewModel.Mapear(usuario);
    }

    public async Task<IEnumerable<EmblemaViewModel>> ObterEmblemas(Guid usuarioId)
    {
        var emblemas = await _usuarioRepository.ObterEmblemasAsync(usuarioId);
        return emblemas.Select(EmblemaViewModel.Mapear);
    }

    public async Task<IEnumerable<MetaViewModel>> ObterMetas(Guid usuarioId)
    {
        var metas = await _usuarioRepository.ObterMetasAsync(usuarioId);
        return metas.Select(MetaViewModel.Mapear);
    }
}