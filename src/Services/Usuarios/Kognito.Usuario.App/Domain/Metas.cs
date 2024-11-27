using System;
using EstartandoDevsCore.DomainObjects;

namespace Kognito.Usuarios.App.Domain;

public class Metas : Entity, IAggregateRoot
{
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }
    public bool Concluida { get; private set; }
    public DateTime CriadaEm { get; private set; }
    public DateTime? ConcluidaEm { get; private set; } //Data e hora em que a meta foi concluída, inicialmente null.
    private Metas() 
    {
        CriadaEm = DateTime.Now;
        Concluida = false;
    }

    public Metas(string titulo, string descricao) : this()
    {
        Titulo = titulo;
        Descricao = descricao;
    }

    public void AtribuirTitulo(string titulo) => Titulo = titulo;

    public void AtribuirDescricao(string descricao) => Descricao = descricao;

    public void MarcarComoConcluida() //Define a meta como concluída e registra o momento atual em ConcluidaEm//
    {
        Concluida = true;
        ConcluidaEm = DateTime.Now;
    }

    public void DesmarcarComoConcluida()
    {
        Concluida = false;
        ConcluidaEm = null;
    }
}
