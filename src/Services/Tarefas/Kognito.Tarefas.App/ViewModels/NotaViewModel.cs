using Kognito.Tarefas.Domain;

namespace Kognito.Tarefas.App.ViewModels;

public class NotaViewModel
{
    public Guid Id { get; set; }
    public double GradeValue { get; set; }
    public Guid StudentId { get; set; }
    public Guid ClassId { get; set; }
    public Guid DeliveryId { get; set; }
    public DateTime AssignedOn { get; set; }

    public static NotaViewModel Mapear(Nota nota)
    {
        return new NotaViewModel
        {
            Id = nota.Id,
            GradeValue = nota.ValorNota,
            StudentId = nota.AlunoId,
            ClassId = nota.TurmaId,
            DeliveryId = nota.EntregaId,
            AssignedOn = nota.AtribuidoEm
        };
    }
}