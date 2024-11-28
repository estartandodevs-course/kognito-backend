using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class AtualizarAlunoCommand : Command
{

    public Guid Id { get; set; }
    public Guid AlunoId { get; set; }
    public string Neurodivergencia { get; set; }
    public int Ofensiva { get; set; }

    public AtualizarAlunoCommand(Guid id, Guid alunoId, string neurodivergencia, int ofensiva)
    {
        Id = id;
        AlunoId = alunoId;
        Neurodivergencia = neurodivergencia;
        Ofensiva = ofensiva;
    }
 
}
