using Kognito.Usuarios.App.Domain;

using Kognito.Usuarios.App.Domain;

namespace Kognito.Usuarios.App.ViewModels;

public class EmblemaViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Unlocked { get; set; }
    public DateTime? UnlockDate { get; set; }
    public Guid UserId { get; set; }

    public EmblemaViewModel() { }

    public EmblemaViewModel(Emblemas emblema)
    {
        Id = emblema.Id;
        Name = emblema.Nome;
        Description = emblema.Descricao;
        Unlocked = emblema.Desbloqueado;
        UnlockDate = emblema.DataDesbloqueio;
        UserId = emblema.UsuarioId;
    }
}