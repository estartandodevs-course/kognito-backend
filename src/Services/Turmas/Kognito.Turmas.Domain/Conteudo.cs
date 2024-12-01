using EstartandoDevsCore.DomainObjects;

namespace Kognito.Turmas.Domain;
public class Conteudo : Entity, IAggregateRoot
{
    public string Titulo { get; private set; }
    public string ConteudoDidatico { get; private set; }
     public Turma Turma { get; private set; }
    public Guid TurmaId { get; private set; }


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
      
    public void VincularTurma(Turma turma)
    {
        Turma = turma ?? throw new DomainException("A turma não pode ser nula");
        TurmaId = turma.Id;
    }
}