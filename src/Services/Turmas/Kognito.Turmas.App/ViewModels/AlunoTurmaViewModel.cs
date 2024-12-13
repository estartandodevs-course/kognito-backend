namespace Kognito.Turmas.App.ViewModels;

public class AlunoTurmaViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }

    public static AlunoTurmaViewModel Mapear(Enturmamento enturmamento)
    {
        return new AlunoTurmaViewModel
        {
            Id = enturmamento.Aluno.Id,
            Name = enturmamento.Aluno.Nome,
            Status = enturmamento.Status.ToString()
        };
    }
}