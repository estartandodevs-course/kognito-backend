using EstartandoDevsCore.DomainObjects;

namespace Kognito.Turmas.Domain;
public class Conteudo : Entity, IAggregateRoot
{
    public string Titulo { get; set; }
    public string ConteudoDidatico { get; set; }
    protected Conteudo(){}
    public Conteudo(string titulo, string conteudoDidatico)
    {
        ValidarTitulo(titulo);
        Titulo = titulo;
        ConteudoDidatico = conteudoDidatico;
    }
    public void ValidarTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new DomainException("O título do conteúdo é obrigatório");
    }


    public void AtribuirTitulo(string titulo)
    {
        ValidarTitulo(titulo);
        Titulo = titulo;
    }

    public void AtribuirConteudoDidatico(string conteudoDidatico) => ConteudoDidatico =conteudoDidatico ?? 
            throw new DomainException("O conteúdo didático não pode ser nulo");
}