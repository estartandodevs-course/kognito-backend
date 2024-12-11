using System;
using System.Text.Json.Serialization;
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
    public string? ResponsavelEmail { get; private set; }
    public Guid? CodigoPai { get; private set; }
    public string? CodigoRecuperacaoEmail { get; private set; }
    public TipoUsuario TipoUsuario { get; private set; }

    
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

    public Usuario(string nome, Cpf cpf, Neurodivergencia? neurodivergencia = null, string? responsavelEmail = null) : this()
    {
        Nome = nome;
        Cpf = cpf;
        Neurodivergencia = neurodivergencia;
        ResponsavelEmail = responsavelEmail;
        if (responsavelEmail != null)
        {
            CodigoPai = Guid.NewGuid();
        }
    }

    public void AtribuirResponsavelEmail(string? email)
    {
        ResponsavelEmail = email;
        if (email != null && CodigoPai == null)
        {
            CodigoPai = Guid.NewGuid();
        }
    }

    public void GerarCodigoRecuperacao()
    {
        CodigoRecuperacaoEmail = Guid.NewGuid().ToString("N");
    }

    public void LimparCodigoRecuperacao()
    {
        CodigoRecuperacaoEmail = null;
    }

    public void AtribuirNome(string nome) => Nome = nome;
    public void AtribuirLogin(Login login) => Login = login;
    public void AtribuirNeurodivergencia(Neurodivergencia? neurodivergencia) => Neurodivergencia = neurodivergencia;
    
    public void AtribuirTipoUsuario(TipoUsuario tipo)
    {
        TipoUsuario = tipo;
    }
    public void AcrescentarOfensiva() => Ofensiva++;
    public void ResetarOfensiva() => Ofensiva = 0;
    
    public void AdicionarEmblema(Emblemas emblema) => _emblemas.Add(emblema);
    public void RemoverEmblema(Emblemas emblema) => _emblemas.Remove(emblema);
    
    public void AdicionarMeta(Metas meta) => _metas.Add(meta);
    public void RemoverMeta(Metas meta) => _metas.Remove(meta);
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TipoUsuario
{
    Student,
    Teacher
}