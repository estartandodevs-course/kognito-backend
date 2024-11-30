using Kognito.Usuarios.App.Domain;

namespace Kognito.Usuarios.App.ViewModels;

public class EmblemaViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public DateTime? DesbloqueadoEm { get; set; }

    public static EmblemaViewModel Mapear(Emblemas emblema)
    {
        if (emblema == null) return null;

        return new EmblemaViewModel
        {
            Id = emblema.Id,
            Nome = emblema.Nome,
            Descricao = emblema.Descricao,
            DesbloqueadoEm = emblema.DesbloqueadoEm
        };
    }
}