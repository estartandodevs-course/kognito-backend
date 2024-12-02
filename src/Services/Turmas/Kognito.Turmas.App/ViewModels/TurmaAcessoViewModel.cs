public class TurmaAcessoViewModel
{
    public Guid TurmaId { get; set; }
    public string LinkAcesso { get; set; }
    public string HashAcesso { get; set; }

    public static TurmaAcessoViewModel Mapear(Turma turma)
    {
        if (turma == null) return null;

        return new TurmaAcessoViewModel
        {
            TurmaId = turma.Id,
            LinkAcesso = turma.LinkAcesso,
            HashAcesso = turma.HashAcesso
        };
    }
}