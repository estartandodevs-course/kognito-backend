using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

public class CriarAlunoCommand : Command 
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public string EmailResponsavel { get; private set; }

    public CriarAlunoCommand(string nome, string cpf, string email, string senha, string emailResponsavel)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Senha = senha;
        EmailResponsavel = emailResponsavel;
    }
}