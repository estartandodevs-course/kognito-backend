using System;
using EstartandoDevsCore.DomainObjects;
using Kognito.Usuarios.App.Domain;

namespace Kognito.Tarefas.Domain;
public class Tarefa : Entity, IAggregateRoot
{
    public string Descricao { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime DataFinalEntrega { get; private set; }
    public DateTime CriadoEm { get; private set; }
    public Guid TurmaId { get; private set; }
    public List<Neurodivergencia> NeurodivergenciasAlvo { get; private set; } = new();
    public ICollection<Entrega> Entregas { get; private set; } = new List<Entrega>();
    
    private Tarefa() { }
    
    public Tarefa(string descricao, string conteudo, DateTime dataFinalEntrega, Guid turmaId, List<Neurodivergencia>? neurodivergenciasAlvo = null) : this()    {
        Descricao = descricao;
        Conteudo = conteudo;
        DataFinalEntrega = dataFinalEntrega;
        TurmaId = turmaId;
        NeurodivergenciasAlvo = neurodivergenciasAlvo ?? new List<Neurodivergencia>();
        CriadoEm = DateTime.Now;
    }

    public Tarefa(Guid id, string descricao, string conteudo, DateTime dataFinalEntrega, Guid turmaId, List<Neurodivergencia>? neurodivergenciasAlvo = null) : this()    {
        Id = id;
        Descricao = descricao;
        Conteudo = conteudo;
        DataFinalEntrega = dataFinalEntrega;
        TurmaId = turmaId;
        NeurodivergenciasAlvo = neurodivergenciasAlvo ?? new List<Neurodivergencia>();
        CriadoEm = DateTime.Now;
    }

    public void AtribuirDescricao(string descricao) => Descricao = descricao;
    public void AtribuirConteudo(string conteudo) => Conteudo = conteudo;
    public void AtribuirDataFinalEntrega(DateTime dataFinalEntrega) => DataFinalEntrega = dataFinalEntrega;
    public void AtribuirTurmaId(Guid turmaId) => TurmaId = turmaId;
    public void AtribuirNeurodivergenciasAlvo(List<Neurodivergencia> neurodivergencias) => NeurodivergenciasAlvo = neurodivergencias;
    public void AdicionarEntrega(Entrega entrega) => Entregas.Add(entrega);
}