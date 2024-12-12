using Kognito.Usuarios.App.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using EstartandoDevsCore.Data;
using Kognito.Usuarios.App.Infra.Data;
using Kognito.Usuarios.App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kognito.Usuarios.App.Infra.Repositories;

public class UsuarioRepository : IUsuariosRepository
{
    private readonly UsuarioContext _context;
    protected readonly DbSet<Usuario> DbSet;
    protected readonly DbSet<Metas> MetasSet;
    protected readonly DbSet<Emblemas> EmblemasSet;

    public UsuarioRepository(UsuarioContext context)
    {
        _context = context;
        DbSet = _context.Set<Usuario>();
        MetasSet = _context.Set<Metas>();
        EmblemasSet = _context.Set<Emblemas>();
    }

    public UsuarioRepository(UsuarioContext context, DbSet<Usuario> dbSet, DbSet<Metas> metasSet, DbSet<Emblemas> emblemasSet)
    {
        _context = context;
        DbSet = dbSet;
        MetasSet = metasSet;
        EmblemasSet = emblemasSet;
    }

    private IUnitOfWorks UnitOfWork => _context;
    IUnitOfWorks IRepository<Emblemas>.UnitOfWork => UnitOfWork;

    IUnitOfWorks IRepository<Usuario>.UnitOfWork => UnitOfWork;

    #region Métodos Usuario

    async Task<Usuario> IRepository<Usuario>.ObterPorId(Guid id)
    {
        return await DbSet
            .Include(u => u.Emblemas)
            .Include(u => u.Metas)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    async Task<IEnumerable<Usuario>> IUsuariosRepository.ObterTodosAsync()
    {
        return await DbSet
            .Include(u => u.Emblemas)
            .Include(u => u.Metas)
            .OrderBy(u => u.Nome)
            .ToListAsync();
    }

    async Task<Usuario> IUsuariosRepository.ObterPorEmail(string email)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Login.Email.Endereco == email);
    }

    async Task<Usuario> IUsuariosRepository.ObterPorCpf(string cpf)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Cpf.Numero == cpf);
    }

    async Task<Usuario> IUsuariosRepository.ObterPorCodigoRecuperacao(string codigo)
    {
        return await DbSet
            .FirstOrDefaultAsync(u => u.CodigoRecuperacaoEmail == codigo);
    }

    async Task<Usuario> IUsuariosRepository.ObterPorCodigoPai(Guid codigoPai)
    {
        return await DbSet
            .FirstOrDefaultAsync(u => u.CodigoPai == codigoPai);
    }

    async Task<IEnumerable<Usuario>> IUsuariosRepository.ObterPorResponsavelEmail(string email)
    {
        return await DbSet
            .Where(u => u.ResponsavelEmail == email)
            .ToListAsync();
    }
    #endregion

    #region Métodos Emblemas

    async Task IEmblemaRepository.AdicionarAsync(Emblemas emblema)
    {
        await EmblemasSet.AddAsync(emblema);
    }

    void IRepository<Emblemas>.Adicionar(Emblemas emblema)
    {
        EmblemasSet.Add(emblema);
    }

    void IRepository<Emblemas>.Atualizar(Emblemas emblema)
    {
        EmblemasSet.Update(emblema);
    }

    void IRepository<Emblemas>.Apagar(Func<Emblemas, bool> predicate)
    {
        var emblemas = EmblemasSet.Where(predicate);
        EmblemasSet.RemoveRange(emblemas);
    }

    async Task<Emblemas> IRepository<Emblemas>.ObterPorId(Guid id)
    {
        return await EmblemasSet.FindAsync(id);
    }

    async Task<IEnumerable<Emblemas>> IEmblemaRepository.ObterEmblemasUsuario(Guid usuarioId)
    {
        return await EmblemasSet
            .Where(e => e.UsuarioId == usuarioId)
            .OrderBy(e => e.OrdemDesbloqueio)
            .ToListAsync();
    }

    async Task<int> IEmblemaRepository.ObterQuantidadeEntregasUsuario(Guid usuarioId)
    {
        return await _context.Entregas
            .CountAsync(e => e.UsuarioId == usuarioId);
    }

    async Task<Emblemas> IEmblemaRepository.ObterProximoEmblemaDisponivel(Guid usuarioId)
    {
        return await EmblemasSet
            .Where(e => e.UsuarioId == usuarioId && !e.Desbloqueado)
            .OrderBy(e => e.OrdemDesbloqueio)
            .FirstOrDefaultAsync();
    }

    async Task<IEnumerable<Emblemas>> IUsuariosRepository.ObterEmblemasAsync(Guid usuarioId)
    {
        var usuario = await ObterPorId(usuarioId);
        return usuario?.Emblemas ?? Enumerable.Empty<Emblemas>();
    }
    #endregion

    #region Métodos Metas

    async Task<IEnumerable<Metas>> IUsuariosRepository.ObterMetasAsync(Guid usuarioId)
    {
        var usuario = await ObterPorId(usuarioId);
        return usuario?.Metas ?? Enumerable.Empty<Metas>();
    }

    async Task<Metas> IUsuariosRepository.ObterMetaPorId(Guid metaId)
    {
        return await MetasSet.FindAsync(metaId);
    }

    void IUsuariosRepository.AdicionarMeta(Metas meta)
    {
        MetasSet.Add(meta);
    }

    void IUsuariosRepository.AtualizarMeta(Metas meta)
    {
        MetasSet.Update(meta);
    }

    void IUsuariosRepository.RemoverMeta(Metas meta)
    {
        MetasSet.Remove(meta);
    }
    #endregion

    #region Métodos Usuario

    private void Adicionar(Usuario usuario)
    {
        DbSet.Add(usuario);
    }

    void IRepository<Usuario>.Adicionar(Usuario usuario)
    {
        Adicionar(usuario);
    }

    void IUsuariosRepository.Adicionar(Usuario usuario)
    {
        Adicionar(usuario);
    }

    private void Atualizar(Usuario usuario)
    {
        DbSet.Update(usuario);
    }

    void IRepository<Usuario>.Atualizar(Usuario usuario)
    {
        Atualizar(usuario);
    }

    void IUsuariosRepository.Atualizar(Usuario usuario)
    {
        Atualizar(usuario);
    }

    private void Apagar(Func<Usuario, bool> predicate)
    {
        var usuarios = DbSet.Where(predicate);
        DbSet.RemoveRange(usuarios);
    }

    void IRepository<Usuario>.Apagar(Func<Usuario, bool> predicate)
    {
        Apagar(predicate);
    }

    void IUsuariosRepository.Apagar(Func<Usuario, bool> predicate)
    {
        Apagar(predicate);
    }

    #endregion

    void IDisposable.Dispose()
    {
        _context?.Dispose();
    }
}