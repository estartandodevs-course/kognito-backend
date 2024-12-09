using Kognito.Tarefas.Domain;


namespace Kognito.Tarefas.App.ViewModels;

public class EntregaViewModel
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime DeliveredOn { get; set; }
    public Guid StudentId { get; set; }
    public Guid TaskId { get; set; }
    public bool IsLate { get; set; }

    public static EntregaViewModel Mapear(Entrega entrega)
    {
        return new EntregaViewModel
        {
            Id = entrega.Id,
            Content = entrega.Conteudo,
            DeliveredOn = entrega.EntregueEm,
            StudentId = entrega.AlunoId,
            TaskId = entrega.TarefaId,
            IsLate = entrega.Atrasada,
        };
    }
}