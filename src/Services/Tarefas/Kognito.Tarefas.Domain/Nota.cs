using EstartandoDevsCore.DomainObjects;

namespace Kognito.Tarefas.Domain;

public class Nota : Entity
{
    public double ValorNota { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TurmaId { get; private set; }
    public Guid EntregaId { get; private set; }
    public DateTime AtribuidoEm { get; private set; }
    public Entrega Entrega { get; private set; }

    private Nota() { }

    public Nota(double valorNota, Guid alunoId, Guid turmaId, Guid entregaId) : this()
    {
        ValorNota = valorNota;
        AlunoId = alunoId;
        TurmaId = turmaId;
        EntregaId = entregaId;
        AtribuidoEm = DateTime.Now;
    }

    public void AtribuirValorNota(double valorNota) 
    {
        ValorNota = valorNota;
        AtribuidoEm = DateTime.Now;
    }
    public void AtribuirAlunoId(Guid alunoId) => AlunoId = alunoId;
    public void AtribuirTurmaId(Guid turmaId) => TurmaId = turmaId;
    public void AtribuirEntregaId(Guid entregaId) => EntregaId = entregaId;
}