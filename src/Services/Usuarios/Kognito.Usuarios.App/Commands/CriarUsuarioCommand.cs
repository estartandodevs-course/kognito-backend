using System;
using EstartandoDevsCore.Messages;

namespace Kognito.Usuarios.App.Commands;

public class CriarUsuarioCommand : Command 
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public string Neurodivergencia { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }

    public CriarUsuarioCommand(string nome, string cpf, string neurodivergencia, string email, string senha)
    {
        Nome = nome;
        Cpf = cpf;
        Neurodivergencia = neurodivergencia;
        Email = email;
        Senha = senha;
    }
}