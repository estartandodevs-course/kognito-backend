namespace Kognito.Usuarios.App.Commands;

using EstartandoDevsCore.Messages;


public class CriarProfessorCommand : Command 
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }

    public CriarProfessorCommand(string nome, string cpf, string email, string senha)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
        Senha = senha;
    }
}