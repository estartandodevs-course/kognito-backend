using EstartandoDevsCore.Messages;

namespace Kognito.Tarefas.App.Events;

public class TarefaEntregueEvent : Event
{
    public Guid TarefaId { get; private set; }
    public Guid AlunoId { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime EntregueEm { get; private set; }
    public bool Atrasada { get; private set; }
    public Guid UsuarioId { get; set; }

    public TarefaEntregueEvent(Guid tarefaId, Guid alunoId, string conteudo, DateTime entregueEm, bool atrasada)
    {
        TarefaId = tarefaId;
        AlunoId = alunoId;
        Conteudo = conteudo;
        EntregueEm = entregueEm;
        Atrasada = atrasada;
    }
}