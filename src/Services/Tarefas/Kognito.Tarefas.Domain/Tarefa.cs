using System;
using EstartandoDevsCore.DomainObjects;

namespace Kognito.Tarefas.Domain;
public class Tarefa : Entity, IAggregateRoot
{
    public string Descricao { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime DataFinalEntrega { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public Guid TurmaId { get; private set; }
    public DateTime DataDeEntrega { get; private set; }
    //public ICollection<Entrega> Entregas { get; private set; } = new List<Entrega>();

    private List<Entrega> _Entregas;
    public IReadOnlyCollection<Entrega> Entregas => _Entregas;
    
    private Tarefa() 
    {
        _Entregas = new List<Entrega>();
    }
    
    public Tarefa(string descricao, string conteudo, DateTime dataFinalEntrega, Guid turmaId) : this()
    {
        Descricao = descricao;
        Conteudo = conteudo;
        DataFinalEntrega = dataFinalEntrega;
        TurmaId = turmaId;
        CriadoEm = DateTime.Now;
    }

    public void AtribuirDescricao(string descricao) => Descricao = descricao;
    public void AtribuirConteudo(string conteudo) => Conteudo = conteudo;
    public void AtribuirDataFinalEntrega(DateTime dataFinalEntrega) => DataFinalEntrega = dataFinalEntrega;
    public void AtribuirTurmaId(Guid turmaId) => TurmaId = turmaId;
    public void AdicionarEntrega(Entrega entrega)
    {
        //Esse aluno já entregou? se já, o que fazer?

        if (Entregas.Any(c => c.AlunoId == entrega.AlunoId)) return;

        _Entregas.Add(entrega);
    }
}