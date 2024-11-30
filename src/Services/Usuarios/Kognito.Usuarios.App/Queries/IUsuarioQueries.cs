﻿using Kognito.Usuarios.App.ViewModels;

namespace Kognito.Usuarios.App.Queries;

public interface IUsuarioQueries
{
    Task<UsuarioViewModel> ObterPorId(Guid id);
    Task<IEnumerable<EmblemaViewModel>> ObterEmblemas(Guid usuarioId);
    Task<IEnumerable<MetaViewModel>> ObterMetas(Guid usuarioId);
}