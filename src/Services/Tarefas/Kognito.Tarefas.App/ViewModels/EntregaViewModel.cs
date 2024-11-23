using Kognito.Tarefas.Domain;


namespace Kognito.Tarefas.App.ViewModels;

public class EntregaViewModel
{
    public Guid Id { get; set; }
    public string Conteudo { get; set; }
    public DateTime EntregueEm { get; set; }
    public Guid AlunoId { get; set; }
    public Guid TarefaId { get; set; }
    public bool Atrasada { get; set; }

    public static EntregaViewModel Mapear(Entrega entrega)
    {
        return new EntregaViewModel
        {
            Id = entrega.Id,
            Conteudo = entrega.Conteudo,
            EntregueEm = entrega.EntregueEm,
            AlunoId = entrega.AlunoId,
            TarefaId = entrega.TarefaId,
            Atrasada = entrega.Atrasada
        };
    }
}