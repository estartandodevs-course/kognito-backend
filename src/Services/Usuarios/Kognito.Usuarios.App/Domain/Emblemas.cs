using System;
using EstartandoDevsCore.DomainObjects;

namespace Kognito.Usuarios.App.Domain;

    public class Emblemas : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Desbloqueado { get; private set; }
    public int OrdemDesbloqueio { get; private set; }
    public DateTime? DesbloqueadoEm { get; private set; }
    
    public Guid UsuarioID { get; private set; }

    private Emblemas() { }

    public Emblemas(string nome, string descricao, Guid usuarioId, int ordemDesbloqueio)
    {
        Nome = nome;
        Descricao = descricao;
        UsuarioId = usuarioId;
        OrdemDesbloqueio = ordemDesbloqueio;
        Desbloqueado = false;
    }

    public Guid UsuarioId { get; set; }

    public void Desbloquear()
    {
        Desbloqueado = true;
        DataDesbloqueio = DateTime.Now;
    }

    public DateTime DataDesbloqueio { get; set; }
}

