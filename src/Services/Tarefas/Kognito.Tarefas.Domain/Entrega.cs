using System;
using EstartandoDevsCore.DomainObjects;

namespace Kognito.Tarefas.Domain;

using System;

public class Entrega : Entity
{
    public string Conteudo { get; private set; }
    public DateTime EntregueEm { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TarefaId { get; private set; }
    public bool Atrasada { get; private set; }

    private Entrega(){ }

    public Entrega(string conteudo, Guid alunoId, Guid tarefaId) : this()
    {
        Conteudo = conteudo;
        AlunoId = alunoId;
        TarefaId = tarefaId;
        EntregueEm = DateTime.Now;
    }

    public void AtribuirConteudo(string conteudo) => Conteudo = conteudo;
    public void AtribuirEntregueEm(DateTime entregueEm) => EntregueEm = entregueEm;
    public void AtribuirAlunoId(Guid alunoId) => AlunoId = alunoId;
    public void AtribuirTarefaId(Guid tarefaId) => TarefaId = tarefaId;

    public void VerificarAtraso(DateTime dataFinalEntrega)
    {
        Atrasada = EntregueEm > dataFinalEntrega;
    }
}