using Kognito.Usuarios.App.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using EstartandoDevsCore.Data;
using Kognito.Usuarios.App.Infra.Data;
using Kognito.Usuarios.App.Domain;

namespace Kognito.Usuarios.App.Infra.Repositories;

public class UsuarioRepository : IUsuariosRepository
{
    private readonly UsuarioContext _context;
    protected readonly DbSet<Usuario> DbSet;

    public UsuarioRepository(UsuarioContext context)
    {
        _context = context;
        DbSet = _context.Set<Usuario>();
    }

    public IUnitOfWorks UnitOfWork => _context;

    public async Task<Usuario> ObterPorId(Guid id)
    {
        return await DbSet
            .Include(u => u.Emblemas)
            .Include(u => u.Metas)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return await DbSet
            .Include(u => u.Emblemas)
            .Include(u => u.Metas)
            .OrderBy(u => u.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Emblemas>> ObterEmblemasAsync(Guid usuarioId)
    {
        var usuario = await ObterPorId(usuarioId);
        return usuario?.Emblemas ?? Enumerable.Empty<Emblemas>();
    }

    public async Task<IEnumerable<Metas>> ObterMetasAsync(Guid usuarioId)
    {
        var usuario = await ObterPorId(usuarioId);
        return usuario?.Metas ?? Enumerable.Empty<Metas>();
    }

    public void Adicionar(Usuario usuario)
    {
        DbSet.Add(usuario);
    }

    public void Atualizar(Usuario usuario)
    {
        DbSet.Update(usuario);
    }

    public void Apagar(Func<Usuario, bool> predicate)
    {
        var usuarios = DbSet.Where(predicate);
        DbSet.RemoveRange(usuarios);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}