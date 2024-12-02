using Kognito.Usuarios.App.Domain;

public class OfensivaViewModel
{
    public Guid UsuarioId { get; set; }
    public int Ofensiva { get; set; }

    public static OfensivaViewModel Mapear(Usuario usuario)
    {
        if (usuario == null) return null;
        
        return new OfensivaViewModel
        {
            UsuarioId = usuario.Id,
            Ofensiva = usuario.Ofensiva
        };
    }
}