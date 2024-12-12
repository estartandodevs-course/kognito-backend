using System;
using EstartandoDevsCore.DomainObjects;

namespace Kognito.Usuarios.App.Domain;

public class Emblemas : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Desbloqueado { get; private set; }
    public int OrdemDesbloqueio { get; private set; }
    public DateTime? DataDesbloqueio { get; private set; }
    public Guid UsuarioId { get; private set; }

    protected Emblemas() { }

    public Emblemas(string nome, string descricao, Guid usuarioId, int ordemDesbloqueio)
    {
        Nome = nome;
        Descricao = descricao;
        UsuarioId = usuarioId;
        OrdemDesbloqueio = ordemDesbloqueio;
        Desbloqueado = false;
    }

    public void Desbloquear()
    {
        Desbloqueado = true;
        DataDesbloqueio = DateTime.Now;
    }
}