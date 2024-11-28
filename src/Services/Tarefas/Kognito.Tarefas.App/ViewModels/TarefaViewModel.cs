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
    public ICollection<EntregaViewModel> Entregas { get; set; } = new List<EntregaViewModel>();

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
            Entregas = tarefa.Entregas?.Select(EntregaViewModel.Mapear).ToList() ?? new List<EntregaViewModel>()
        };
    }
}