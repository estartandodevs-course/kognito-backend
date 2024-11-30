using Kognito.Turmas.Domain;
using static Enturmamento;

namespace Kognito.Turmas.App.ViewModels;

public class EnturmamentoViewModel
{
    public Guid Id { get; set; }
    public Usuario Aluno { get; set; }
    public Turma Turma { get; set; }
    public EnturtamentoStatus Status { get; set; }

    public static EnturmamentoViewModel Mapear(Enturmamento enturmamento)
    {
        if(enturmamento == null)
            return null;

        return new EnturmamentoViewModel
        {
            Id = enturmamento.Id,
            Aluno = enturmamento.Aluno,
            Turma = enturmamento.Turma,
            Status = enturmamento.Status,
            
        };
    }
}
