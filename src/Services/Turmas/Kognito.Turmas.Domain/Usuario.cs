using System;
using EstartandoDevsCore.DomainObjects;
using EstartandoDevsCore.ValueObjects;

namespace Kognito.Turmas.Domain;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    protected Usuario(){}

    public Usuario(string nome, Guid id)
    {
        ValidarNome(nome);
        Nome = nome;
        Id = id;

    }
    private void ValidarNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio ou nulo");
    }
}
