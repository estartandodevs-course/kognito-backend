public class TurmaAcessoViewModel
{
    public Guid ClassId { get; set; }
    public string AccessLink { get; set; }
    public string AccessHash { get; set; }

    public static TurmaAcessoViewModel Mapear(Turma turma)
    {
        if (turma == null) return null;

        return new TurmaAcessoViewModel
        {
            ClassId = turma.Id,
            AccessLink = turma.LinkAcesso,
            AccessHash = turma.HashAcesso
        };
    }
}