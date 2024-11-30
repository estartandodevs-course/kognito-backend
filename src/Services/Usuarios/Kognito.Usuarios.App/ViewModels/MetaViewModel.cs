using Kognito.Usuarios.App.Domain;

namespace Kognito.Usuarios.App.ViewModels;

public class MetaViewModel
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool Concluida { get; set; }
    public DateTime CriadaEm { get; set; }
    public DateTime? ConcluidaEm { get; set; }

    public static MetaViewModel Mapear(Metas meta)
    {
        if (meta == null) return null;

        return new MetaViewModel
        {
            Id = meta.Id,
            Titulo = meta.Titulo,
            Descricao = meta.Descricao,
            Concluida = meta.Concluida,
            CriadaEm = meta.CriadaEm,
            ConcluidaEm = meta.ConcluidaEm
        };
    }
}