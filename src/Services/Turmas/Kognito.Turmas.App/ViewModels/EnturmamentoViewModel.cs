using System;
using Kognito.Turmas.Domain;
using static Kognito.Turmas.Domain.Enturmamento;

namespace Kognito.Turmas.App.ViewModels;

public class EnturmamentoViewModel
{
    public Guid Id { get; set; }
    public Usuario Aluno { get; set; }
    public EnturtamentoStatus Status { get; set; }

    public static EnturmamentoViewModel Mapear(Enturmamento enturmamento)
    {
        return new EnturmamentoViewModel
        {
            Id = enturmamento.Id,
            Aluno = enturmamento.Aluno,
            Status = enturmamento.Status,
            
        };
    }
}
