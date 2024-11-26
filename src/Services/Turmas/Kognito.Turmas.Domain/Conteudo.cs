using EstartandoDevsCore.DomainObjects;

namespace Kognito.Turmas.Domain;
public class Conteudo : Entity, IAggregateRoot
{
    public string Titulo { get; set; }
    public string ConteudoDidatico { get; set; }

    public Conteudo(string titulo, string conteudoDidatico)
    {
        Titulo = titulo;
        ConteudoDidatico = conteudoDidatico;
    }
    private Conteudo(){}

    public void TituloObrigatorio(string titulo)
    {
        if(string.IsNullOrWhiteSpace(titulo))
        {
            throw new DomainException("O título do conteúdo é obrigatorio");

            Titulo = titulo;
        }
    }

    public void AtribuirConteudoDidatico(string conteudoDidatico) => ConteudoDidatico =conteudoDidatico;
}