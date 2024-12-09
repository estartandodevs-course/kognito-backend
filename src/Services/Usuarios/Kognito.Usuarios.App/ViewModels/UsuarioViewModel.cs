using Kognito.Usuarios.App.Domain;

namespace Kognito.Usuarios.App.ViewModels;

public class UsuarioViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string Email { get; set; }
    public string? Neurodivergencia { get; set; }
    public TipoUsuario Role { get; set; }

    public static UsuarioViewModel Mapear(Usuario usuario)
    {
        if (usuario == null) return null;
        
        return new UsuarioViewModel
        {
            Id = usuario.Id,
            Role = usuario.TipoUsuario,
            Name = usuario.Nome,
            Cpf = usuario.Cpf.Numero,
            Email = usuario.Login?.Email?.Endereco,
            Neurodivergencia = usuario.Neurodivergencia?.ToString()
        };
    }
}