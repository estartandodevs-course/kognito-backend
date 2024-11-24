using EstartandoDevsCore.Messages;

namespace Kognito.Usuario.App.Commands;

public class CriarUsuarioCommand : Command 
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public string Neurodivergencia { get; set; }

    public CriarUsuarioCommand(string nome, string cpf, string neurodivergencia)
    {
        Nome = nome;
        Cpf = cpf;
        Neurodivergencia = neurodivergencia;
    }
}

