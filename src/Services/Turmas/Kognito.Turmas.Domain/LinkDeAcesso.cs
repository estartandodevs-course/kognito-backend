using System.Security.Cryptography;
using EstartandoDevsCore.DomainObjects;

public class LinkDeAcesso
{
    public string Codigo { get; private set; }
    public DateTime DataExpiracao { get; private set; }
    public bool Ativo { get; private set; }
    public int LimiteUsos { get; private set; }
    public int QuantidadeUsos { get; private set; }
    public DateTime DataCriacao { get; private set; }

    protected LinkDeAcesso() { }

    public LinkDeAcesso(int limiteUsos = 0, int diasValidade = 1)
    {
        
        Codigo = GerarCodigo();
        DataExpiracao = DateTime.UtcNow.AddDays(diasValidade);
        DataCriacao = DateTime.UtcNow;
        Ativo = true;
        LimiteUsos = limiteUsos;
        QuantidadeUsos = 0;
    }

    private string GerarCodigo()
    {
        var codigo = new byte[4];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(codigo);
        }
        return Convert.ToBase64String(codigo).Replace("/","_").Replace("+","-");
    }

    public bool PodeSerUtilizado()
    {
        if (!Ativo) return false;
        if (DateTime.UtcNow > DataExpiracao) return false;
        if (LimiteUsos > 0 && QuantidadeUsos >= LimiteUsos) return false;
        
        return true;
    }

    public void Utilizar()
    {
        if (!PodeSerUtilizado())
            throw new DomainException("Link de acesso inválido ou expirado");

        QuantidadeUsos++;
    }

    public void Desativar() => Ativo = false;

    public void Reativar() => Ativo = true;

    public void EstenderValidade(int diasAdicionais)
    {
        if (diasAdicionais <= 0)
            throw new DomainException("Quantidade de dias inválida");

        DataExpiracao = DataExpiracao.AddDays(diasAdicionais);
    }

    public void AumentarLimiteUsos(int usosAdicionais)
    {
        if (usosAdicionais <= 0)
            throw new DomainException("Quantidade de usos inválida");

        LimiteUsos += usosAdicionais;
    }

    public string ObterLinkCompleto(Guid turmaId)
    {
        return $"/turma/ingressar/{turmaId}/{Codigo}";
    }

    public TimeSpan ObterTempoRestante()
    {
        return DataExpiracao - DateTime.UtcNow;
    }

    public int ObterUsosRestantes()
    {
        if (LimiteUsos == 0) return -1; 
        return LimiteUsos - QuantidadeUsos;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (LinkDeAcesso)obj;
        return Codigo == other.Codigo;
    }

    public override int GetHashCode()
    {
        return Codigo.GetHashCode();
    }
}