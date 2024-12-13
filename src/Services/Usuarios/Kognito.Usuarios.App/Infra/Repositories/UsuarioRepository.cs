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
    protected readonly DbSet<Metas> MetasSet;

    public UsuarioRepository(UsuarioContext context)
    {
        _context = context;
        DbSet = _context.Set<Usuario>();
        MetasSet = _context.Set<Metas>();
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
    
    public async Task<Usuario> ObterPorEmail(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Login.Email.Endereco == email);
    }

    public async Task<Usuario> ObterPorCpf(string cpf)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Cpf.Numero == cpf);
    }
    
    public async Task<Usuario> ObterPorCodigoRecuperacao(string codigo)
    {
        return await DbSet
            .FirstOrDefaultAsync(u => u.CodigoRecuperacaoEmail == codigo);
    }

    public async Task<Usuario> ObterPorCodigoPai(Guid codigoPai)
    {
        return await DbSet
            .FirstOrDefaultAsync(u => u.CodigoPai == codigoPai);
    }

    public async Task<IEnumerable<Usuario>> ObterPorResponsavelEmail(string email)
    {
        return await DbSet
            .Where(u => u.ResponsavelEmail == email)
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

    public async Task<Metas> ObterMetaPorId(Guid metaId)
    {
        return await MetasSet.FindAsync(metaId);
    }

    public void Adicionar(Usuario usuario)
    {
        DbSet.Add(usuario);
    }

    public void Atualizar(Usuario usuario)
    {
        DbSet.Update(usuario);
    }

    public void AdicionarMeta(Metas meta)
    {
        MetasSet.Add(meta);
    }

    public void AtualizarMeta(Metas meta)
    {
        MetasSet.Update(meta);
    }

    public void RemoverMeta(Metas meta)
    {
        MetasSet.Remove(meta);
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