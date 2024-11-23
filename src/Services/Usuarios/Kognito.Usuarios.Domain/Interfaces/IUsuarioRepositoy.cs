using System;
using EstartandoDevsCore.Data;

namespace Kognito.Usuario.Domain.interfaces;
public interface IUsuarioRepository : IRepository<Usuario>, IDisposable
{
    Task<IEnumerable<Usuario>> ObterTodos();
}