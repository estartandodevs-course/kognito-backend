using EstartandoDevsCore.Messages;
using FluentValidation.Results;
using Kognito.Turmas.Domain;

public class CriarTurmaCommand : Command
{
    public Guid Id { get; private set; }
    public Usuario Professor { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public string LinkAcesso { get; private set; }
    public Cor Cor { get; private set; }
    public Icones Icone { get; private set; }

    public CriarTurmaCommand(
        Guid id, 
        Usuario professor, 
        string nome, 
        string descricao, 
        string materia, 
        string linkAcesso,
        Cor cor,
        Icones icone)
    {
        ValidarParametros(id, professor, nome, materia);
        Id = id;
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        LinkAcesso = linkAcesso;
        Cor = cor;
        Icone = icone;
    }

    private void ValidarParametros(Guid id, Usuario professor, string nome, string materia)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(id));
            
        if (professor == null)
            throw new ArgumentException("Professor não pode ser nulo", nameof(professor));
            
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio", nameof(nome));
            
        if (string.IsNullOrWhiteSpace(materia))
            throw new ArgumentException("Matéria não pode ser vazia", nameof(materia));
    }
}