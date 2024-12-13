using Kognito.Turmas.Domain;
using static Enturmamento;

namespace Kognito.Turmas.App.ViewModels;

public class EnturmamentoViewModel
{
    public Guid Id { get; set; }
    public Usuario Student { get; set; }
    public Turma Class { get; set; }
    public EnturtamentoStatus Status { get; set; }

    public static EnturmamentoViewModel Mapear(Enturmamento enturmamento)
    {
        if(enturmamento == null)
            return null;

        return new EnturmamentoViewModel
        {
            Id = enturmamento.Id,
            Student = enturmamento.Aluno,
            Class = enturmamento.Turma,
            Status = enturmamento.Status,
            
        };
    }
}
