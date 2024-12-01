using System;
using EstartandoDevsCore.DomainObjects;
using EstartandoDevsCore.ValueObjects;

namespace Kognito.Usuarios.App.Domain;

public class Usuario : Entity, IAggregateRoot
{
    public string Nome { get; private set; }
    public Cpf Cpf { get; private set; }
    public Login Login { get; private set; }
    public Neurodivergencia? Neurodivergencia { get; private set; }
    public int Ofensiva { get; private set; }
    
    private HashSet<Emblemas> _emblemas;
    private HashSet<Metas> _metas;
    
    public IReadOnlyCollection<Emblemas> Emblemas => _emblemas;
    public IReadOnlyCollection<Metas> Metas => _metas;

    private Usuario()
    {
        _emblemas = new HashSet<Emblemas>();
        _metas = new HashSet<Metas>();
        Ofensiva = 0;
    }

    public Usuario(string nome, Cpf cpf, Neurodivergencia? neurodivergencia = null) : this()
    {
        Nome = nome;
        Cpf = cpf;
        Neurodivergencia = neurodivergencia;
    }

    public void AtribuirNome(string nome) => Nome = nome;
    public void AtribuirLogin(Login login) => Login = login;
    public void AtribuirNeurodivergencia(Neurodivergencia? neurodivergencia) => Neurodivergencia = neurodivergencia;
    
    public void AcrescentarOfensiva() => Ofensiva++;
    public void ResetarOfensiva() => Ofensiva = 0;
    
    public void AdicionarEmblema(Emblemas emblema) => _emblemas.Add(emblema);
    public void RemoverEmblema(Emblemas emblema) => _emblemas.Remove(emblema);
    
    public void AdicionarMeta(Metas meta) => _metas.Add(meta);
    public void RemoverMeta(Metas meta) => _metas.Remove(meta);
}