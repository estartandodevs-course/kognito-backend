using System;
using EstartandoDevsCore.DomainObjects;
using EstartandoDevsCore.ValueObjects;

namespace Kognito.Turmas.Domain;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }

    public Usuario(string nome, Guid id)
    {
        Nome = !string.IsNullOrWhiteSpace(nome) ? nome : "Nome não informado";
        Id = id;
    }
}