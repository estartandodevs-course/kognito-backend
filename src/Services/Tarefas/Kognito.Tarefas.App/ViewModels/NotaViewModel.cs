using Kognito.Tarefas.Domain;

namespace Kognito.Tarefas.App.ViewModels;

public class NotaViewModel
{
    public Guid Id { get; set; }
    public double ValorNota { get; set; }
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public Guid EntregaId { get; set; }
    public DateTime AtribuidoEm { get; set; }

    public static NotaViewModel Mapear(Nota nota)
    {
        return new NotaViewModel
        {
            Id = nota.Id,
            ValorNota = nota.ValorNota,
            AlunoId = nota.AlunoId,
            TurmaId = nota.TurmaId,
            EntregaId = nota.EntregaId,
            AtribuidoEm = nota.AtribuidoEm
        };
    }
}