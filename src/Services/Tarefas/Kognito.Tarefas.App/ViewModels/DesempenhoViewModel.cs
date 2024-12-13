using Kognito.Tarefas.Domain;

namespace Kognito.Tarefas.App.ViewModels;

public class DesempenhoViewModel
{
    public int TotalAssignments { get; set; }
    public int SubmitedAssignments { get; set; }
    public int lateAssignments { get; set; }
    public int PendingAssignments { get; set; }

    public static DesempenhoViewModel Mapear(IEnumerable<Tarefa> tarefas, Guid? alunoId = null)
    {
        var hoje = DateTime.Now;
        var tarefasDoAluno = tarefas.ToList();
        
        var totalTarefas = tarefasDoAluno.Count;
        var tarefasEntregues = tarefasDoAluno.Count(t => 
            t.Entregas.Any(e => (!alunoId.HasValue || e.AlunoId == alunoId)));
            
        var tarefasAtrasadas = tarefasDoAluno.Count(t => 
            t.DataFinalEntrega < hoje && 
            (!alunoId.HasValue || !t.Entregas.Any(e => e.AlunoId == alunoId)));
            
        var tarefasPendentes = totalTarefas - tarefasEntregues;

        return new DesempenhoViewModel
        {
            TotalAssignments = totalTarefas,
            SubmitedAssignments = tarefasEntregues,
            lateAssignments = tarefasAtrasadas,
            PendingAssignments = tarefasPendentes
        };
    }
}