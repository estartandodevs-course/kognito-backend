using Kognito.Usuario.Domain;

namespace Kognito.Usuario.App.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Neurodivergencia { get; set; }
        public string Email { get; set; }

        public UsuarioViewModel() { }

        public UsuarioViewModel(Guid id, string nome, string cpf, string neurodivergencia, string email)
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Neurodivergencia = neurodivergencia;
            Email = email;
        }
    }
}

