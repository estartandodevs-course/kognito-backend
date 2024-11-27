using System;
using EstartandoDevsCore.DomainObjects;
using EstartandoDevsCore.ValueObjects;

namespace Kognito.Turmas.Domain;

public class Usuario : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public Cpf Cpf { get; private set; }
    public string Neurodivergencia { get; private set; }

    private Usuario(){}

    public Usuario(string nome, Cpf cpf, string neurodivergencia)
    {
        Nome = nome;
        Cpf = cpf;
        Neurodivergencia = neurodivergencia;
    }

    public void AtribuirNome (string nome) => Nome = nome;
    public void AtribuirNeurodivergencia(string neurodivergencia) => Neurodivergencia = neurodivergencia;
}
