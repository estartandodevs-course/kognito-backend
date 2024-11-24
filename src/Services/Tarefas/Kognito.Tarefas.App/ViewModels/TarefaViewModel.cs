using Kognito.Tarefas.Domain;

namespace Kognito.Tarefas.App.ViewModels;

public class TarefaViewModel
{
    public Guid Id { get; set; }
    public string Descricao { get; set; }
    public string Conteudo { get; set; }
    public DateTime DataFinalEntrega { get; set; }
    public DateTime CriadoEm { get; set; }
    public Guid TurmaId { get; set; }
    public EntregaViewModel Entrega { get; set; }
    public NotaViewModel Nota { get; set; }

    public static TarefaViewModel Mapear(Tarefa tarefa)
    {
        return new TarefaViewModel
        {
            Id = tarefa.Id,
            Descricao = tarefa.Descricao,
            Conteudo = tarefa.Conteudo,
            DataFinalEntrega = tarefa.DataFinalEntrega,
            CriadoEm = tarefa.CriadoEm,
            TurmaId = tarefa.TurmaId,
            Entrega = tarefa.Entrega != null ? EntregaViewModel.Mapear(tarefa.Entrega) : null,
            Nota = tarefa.Nota != null ? NotaViewModel.Mapear(tarefa.Nota) : null
        };
    }
}