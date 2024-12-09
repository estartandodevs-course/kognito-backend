using EstartandoDevsCore.Data;
using Kognito.Tarefas.Domain;
using Kognito.Tarefas.Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using Kognito.Tarefas.Infra.Data;

namespace Kognito.Tarefas.Domain.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly TarefasContext _context;
    protected readonly DbSet<Tarefa> DbSet;

    public TarefaRepository(TarefasContext context)
    {
        _context = context;
        DbSet = _context.Set<Tarefa>();
    }

    public IUnitOfWorks UnitOfWork => _context;

    public async Task<IEnumerable<Tarefa>> ObterTodosAsync()
    {
        return await DbSet
            .Include(t => t.Entregas)
                .ThenInclude(e => e.Notas)
            .ToListAsync();
    }

    public async Task<Tarefa> ObterPorIdAsync(Guid id)
    {
        return await DbSet
            .Include(t => t.Entregas)
                .ThenInclude(e => e.Notas)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Tarefa>> ObterPorTurmaAsync(Guid turmaId)
    {
        return await DbSet
            .Include(t => t.Entregas)
                .ThenInclude(e => e.Notas)
            .Where(t => t.TurmaId == turmaId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> ObterPorAlunoAsync(Guid alunoId)
    {
        return await DbSet
            .Include(t => t.Entregas)
                .ThenInclude(e => e.Notas)
            .Where(t => t.Entregas.Any(e => e.AlunoId == alunoId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Tarefa>> ObterTarefasPorTurma(Guid turmaId)
    {
        return await DbSet
            .Include(t => t.Entregas)
            .ThenInclude(e => e.Notas)
            .Where(t => t.TurmaId == turmaId)
            .ToListAsync();
    }
    public async Task<Entrega> ObterEntregaPorIdAsync(Guid entregaId)
    {
        return await _context.Entregas
            .Include(e => e.Notas)
            .FirstOrDefaultAsync(e => e.Id == entregaId);
    }

    public void Adicionar(Tarefa tarefa)
    {
        DbSet.Add(tarefa);
    }

    public void Atualizar(Tarefa tarefa)
    {
        DbSet.Update(tarefa);
    }

    public void Apagar(Func<Tarefa, bool> predicate)
    {
        var entities = DbSet.Where(predicate);
        DbSet.RemoveRange(entities);
    }

    public async Task<Tarefa> ObterPorId(Guid id)
    {
        return await ObterPorIdAsync(id);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}