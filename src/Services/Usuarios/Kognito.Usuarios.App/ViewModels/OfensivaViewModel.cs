using Kognito.Usuarios.App.Domain;

public class OfensivaViewModel
{
    public Guid UserId { get; set; }
    public int Streak { get; set; }

    public static OfensivaViewModel Mapear(Usuario usuario)
    {
        if (usuario == null) return null;
        
        return new OfensivaViewModel
        {
            UserId = usuario.Id,
            Streak = usuario.Ofensiva
        };
    }
}