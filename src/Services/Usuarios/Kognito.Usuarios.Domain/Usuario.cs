using System;
using EstartandoDevsCore.DomainObjects;

namespace Kognito.Usuarios.Domain
public class Usuario : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public Cpf Cpf { get; private set; }
    public Login Login { get; private set; }
    public string Neurodivergencia { get; private set; }

    private Usuario() { }

    public Usuario(string nome, Cpf cpf, string neurodivergencia) : this()
    {
        Nome = nome;
        Cpf = cpf;
        Neurodivergencia = neurodivergencia;
    }

    public void AtribuirNome(string nome) => Nome = nome;

    public void AtribuirLogin(Login login) => Login = login;

    public void AtribuirNeurodivergencia(string neurodivergencia) => Neurodivergencia = neurodivergencia;
}
