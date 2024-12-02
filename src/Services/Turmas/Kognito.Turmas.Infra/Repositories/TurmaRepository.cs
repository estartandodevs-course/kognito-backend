using System.Data.Common;
using EstartandoDevsCore.Data;
using Kognito.Turmas.Domain.Interfaces;
using Kognito.Turmas.Infra.Data;
using Microsoft.EntityFrameworkCore;


public class TurmaRepository : ITurmaRepository
{
    private readonly TurmaContext _context;

    public TurmaRepository(TurmaContext context)
    {
        _context = context;
    }

    public IUnitOfWorks UnitOfWork => _context;

    
    public void Adicionar(Turma turma)
    {
        _context.Turmas.Add(turma);
    }

    public void Atualizar(Turma turma)
    {
        _context.Turmas.Update(turma);
    }

    public void Apagar(Func<Turma, bool> predicate)
    {
        var entities = _context.Turmas.Where(predicate);
        _context.Turmas.RemoveRange(entities);
    }

    public async Task<Turma> ObterPorId(Guid id)
    {
        return await _context.Turmas
            .Include(t => t.Professor)
            .Include(t => t.Enturmamentos)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    
    public async Task<DbConnection> ObterConexao()
    {
        return _context.Database.GetDbConnection();
    }

    public async Task<int> ObterQuantidadeAlunos(Guid turmaId)
    {
        return await _context.Turmas
            .Where(t => t.Id == turmaId)
            .SelectMany(t => t.Enturmamentos)
            .CountAsync();
    }

    public async Task<IEnumerable<Turma>> ObterTodos()
    {
        return await _context.Turmas
            .Include(t => t.Professor)
            .Include(t => t.Enturmamentos)
            .ToListAsync();
    }

    public async Task<IEnumerable<Turma>> ObterTurmasPorProfessor(Guid professorId)
    {
        return await _context.Turmas
            .Include(t => t.Professor)
            .Include(t => t.Enturmamentos)
            .Where(t => t.Professor.Id == professorId)
            .ToListAsync();
    }
    public async Task<Turma> ObterPorHashAcesso(string hash)
    {
        return await _context.Turmas
            .Include(t => t.Professor)
            .FirstOrDefaultAsync(t => t.HashAcesso == hash);
    }

    public void Dispose()
    {
        _context?.Dispose();
        GC.SuppressFinalize(this);
    }
}
