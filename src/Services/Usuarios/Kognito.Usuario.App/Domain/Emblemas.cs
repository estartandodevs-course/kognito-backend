using EstartandoDevsCore.DomainObjects;

namespace Kognito.Usuarios.App.Domain;

    public class Emblemas : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public DateTime? DesbloqueadoEm { get; private set; }

    private Emblemas() { }

    public Emblemas(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
        DesbloqueadoEm = null; //Emblema bloqueado.
    }

    public void Desbloquear()
    {
        if (DesbloqueadoEm == null)
        {
            DesbloqueadoEm = DateTime.Now; //Desbloqueado.
        }
    }

}

