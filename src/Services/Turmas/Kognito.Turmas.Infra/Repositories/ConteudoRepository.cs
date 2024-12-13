// TurmaRepository.cs
using System.Data.Common;
using EstartandoDevsCore.Data;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Interfaces;
using Kognito.Turmas.Infra.Data;
using Microsoft.EntityFrameworkCore;

public class ConteudoRepository : IConteudoRepository
{
     private readonly TurmaContext _context;

    public ConteudoRepository(TurmaContext context)
    {
        _context = context;
    }

    public IUnitOfWorks UnitOfWork => _context;

    public void Adicionar(Conteudo conteudo)
    {
        _context.Conteudos.Add(conteudo);
    }

    public void Atualizar(Conteudo conteudo)
    {
        _context.Conteudos.Update(conteudo);
    }

    public void Apagar(Func<Conteudo, bool> predicate)
    {
        var entities = _context.Conteudos.Where(predicate);
        _context.Conteudos.RemoveRange(entities);
    }

    public async Task<Conteudo> ObterPorId(Guid id)
    {
        return await _context.Conteudos
            .Include(c => c.Turma)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Conteudo>> ObterTodosConteudo()
    {
        return await _context.Conteudos
            .Include(c => c.Turma)
            .ToListAsync();
    }

    public async Task<IEnumerable<Conteudo>> ObterPorTurma(Guid turmaId)
    {
        return await _context.Conteudos
            .Include(c => c.Turma)
            .Where(c => c.TurmaId == turmaId)
            .ToListAsync();
    }

    public async Task<int> ObterQuantidadeConteudosPorTurma(Guid turmaId)
    {
        return await _context.Conteudos
            .Where(c => c.TurmaId == turmaId)
            .CountAsync();
    }

    public async Task<DbConnection> ObterConexao()
    {
        return _context.Database.GetDbConnection();
    }

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }
}