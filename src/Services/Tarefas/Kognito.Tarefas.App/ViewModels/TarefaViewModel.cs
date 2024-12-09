using Kognito.Tarefas.Domain;

namespace Kognito.Tarefas.App.ViewModels;

public class TarefaViewModel
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public DateTime FinalDeliveryDate { get; set; }
    public DateTime CreatedOn { get; set; }
    public Guid ClassId { get; set; }

    public static TarefaViewModel Mapear(Tarefa tarefa)
    {
        return new TarefaViewModel
        {
            Id = tarefa.Id,
            Description = tarefa.Descricao,
            Content = tarefa.Conteudo,
            FinalDeliveryDate = tarefa.DataFinalEntrega,
            CreatedOn = tarefa.CriadoEm,
            ClassId = tarefa.TurmaId,
        };
    }
}