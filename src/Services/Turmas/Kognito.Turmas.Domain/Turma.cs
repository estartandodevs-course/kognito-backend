using EstartandoDevsCore.DomainObjects;

namespace Kognito.Turmas.Domain;

public class Turma : Entity, IAggregateRoot
{ 
    //Tirar esse string e colocar Usuario
    public string Professor{ get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string Materia { get; set; }
    public string LinkAcesso { get; set; }

    public Turma(string professor, string nome, string descricao, string materia, string linkAcesso)
    {
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
    }
    private Turma(){}

    public void AtribuirProfessor(string professor) => Professor = professor;
    public void AtribuirNome(string nome) => Nome = nome;
    public void AtribuirDescricao(string descricao) => Descricao = descricao;
    public void AtribuirMateria(string materia) => Materia = materia;
    public void AtribuirLinkAcesso(string linkAcesso) => LinkAcesso = linkAcesso;
}