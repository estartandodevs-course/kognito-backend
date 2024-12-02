using Kognito.Usuarios.App.Domain;

namespace Kognito.Usuarios.App.ViewModels;

public class MetaViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }

    public static MetaViewModel Mapear(Metas meta)
    {
        if (meta == null) return null;

        return new MetaViewModel
        {
            Id = meta.Id,
            Title = meta.Titulo,
            Description = meta.Descricao,
            Completed = meta.Concluida,
            CreatedAt = meta.CriadaEm,
            CompletedAt = meta.ConcluidaEm
        };
    }
}