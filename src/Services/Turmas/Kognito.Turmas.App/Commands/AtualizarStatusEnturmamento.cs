using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain;
using static Kognito.Turmas.Domain.Enturmamento;

namespace Kognito.Turmas.App.Commands;

public class AtualizarStatusEnturmamentoCommand : Command
{

    public Guid Id { get; set; }
    // public Usuario Aluno { get; set; }
    // public Turma Turma { get; set; }
    public EnturtamentoStatus Status { get; set; }

        public AtualizarStatusEnturmamentoCommand(Guid id,  EnturtamentoStatus status)
    {
        Id = id;
        Status = status;
    }
}
