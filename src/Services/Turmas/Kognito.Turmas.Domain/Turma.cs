using EstartandoDevsCore.DomainObjects;


namespace Kognito.Turmas.Domain;

public class Turma : Entity, IAggregateRoot
{ 

    public Usuario Professor{ get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public string LinkAcesso { get; private set; }
    public EnumParaCores Cor { get; private set; }
    public EnumParaIcones Icones { get; private set; }

   
    protected Turma(){}

    public Turma(Usuario professor, string nome, string descricao, string materia, string linkAcesso, EnumParaCores cor, EnumParaIcones icones)
    {
        ValidarCampos(nome, materia);
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
        Cor = cor;
        Icones = icones;
    }
    private void ValidarCampos(string nome, string materia)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio");
        if (string.IsNullOrWhiteSpace(materia))
            throw new ArgumentException("Matéria não pode ser vazia");
    }


    public void AtribuirProfessor(Usuario professor) => Professor = professor ?? 
    throw new ArgumentNullException(nameof(professor));
    public void AtribuirNome(string nome) => Nome = !string.IsNullOrWhiteSpace(nome) ? 
    nome : throw new ArgumentException("Nome inválido");
    public void AtribuirDescricao(string descricao) => Descricao = descricao;
    public void AtribuirMateria(string materia) => Materia = !string.IsNullOrWhiteSpace(materia) ? 
    materia : throw new ArgumentException("Matéria inválida");
    public void AtribuirLinkAcesso(string linkAcesso) => LinkAcesso = linkAcesso;
    public void AtribuirCor(EnumParaCores cor) => Cor = cor;
    public void AtribuirIcone(EnumParaIcones icones) => Icones = icones;
}