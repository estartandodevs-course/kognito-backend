namespace Kognito.Usuario.App.ViewModels;

public class MetaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool Concluida { get; set; }
    public DateTime CriadaEm { get; set; }
    public DateTime? ConcluidaEm { get; set; }

    public MetaViewModel()
    {
    }

    public MetaViewModel(Guid id, string titulo, string descricao, bool concluida, DateTime criadaEm,
        DateTime? concluidaEm)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        Concluida = concluida;
        CriadaEm = criadaEm;
        ConcluidaEm = concluidaEm;
    }
}