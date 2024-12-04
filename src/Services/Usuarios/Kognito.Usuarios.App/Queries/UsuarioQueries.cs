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

    public async Task<UsuarioViewModel> ObterPorEmail(string email)
    {
        var usuario = await _usuarioRepository.ObterPorEmail(email);
        return UsuarioViewModel.Mapear(usuario);
    }
    
    public async Task<OfensivaViewModel> ObterOfensiva(Guid usuarioId)
    {
        var usuario = await _usuarioRepository.ObterPorId(usuarioId);
        return OfensivaViewModel.Mapear(usuario);
    }
    
    public async Task<string> ObterCodigoRecuperacaoPorEmail(string email)
    {
        var usuario = await _usuarioRepository.ObterPorEmail(email);
        return usuario?.CodigoRecuperacaoEmail;
    }

    public async Task<Guid?> ObterCodigoPaiPorEmail(string email)
    {
        var usuario = await _usuarioRepository.ObterPorEmail(email);
        return usuario?.CodigoPai;
    }

    public async Task<string> ObterResponsavelEmailPorId(Guid id)
    {
        var usuario = await _usuarioRepository.ObterPorId(id);
        return usuario?.ResponsavelEmail;
    }

}