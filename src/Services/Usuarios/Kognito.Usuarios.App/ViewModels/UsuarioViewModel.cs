using Kognito.Usuarios.App.Domain;
using EstartandoDevsCore.ValueObjects;

namespace Kognito.Usuarios.App.ViewModels;

public class UsuarioViewModel
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public Neurodivergencia? Neurodivergencia { get; set; }
    public string Email { get; set; }

    public static UsuarioViewModel Mapear(Usuario usuario)
    {
        if (usuario == null) return null;
        
        return new UsuarioViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Cpf = usuario.Cpf.Numero,
            Neurodivergencia = usuario.Neurodivergencia.HasValue ? usuario.Neurodivergencia.Value : null,
            Email = usuario.Login?.Email?.Endereco,
        };
    }
}