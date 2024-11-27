using EstartandoDevsCore.DomainObjects;


namespace Kognito.Turmas.Domain;

public class Turma : Entity, IAggregateRoot
{ 
    //Tirar esse string e colocar Usuario
    public Usuario Professor{ get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public string LinkAcesso { get; private set; }

    public Turma(Usuario professor, string nome, string descricao, string materia, string linkAcesso)
    {
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
    }
    private Turma(){}

    public void AtribuirProfessor(string professor) => Professor = Professor;
    public void AtribuirNome(string nome) => Nome = nome;
    public void AtribuirDescricao(string descricao) => Descricao = descricao;
    public void AtribuirMateria(string materia) => Materia = materia;
    public void AtribuirLinkAcesso(string linkAcesso) => LinkAcesso = linkAcesso;
}