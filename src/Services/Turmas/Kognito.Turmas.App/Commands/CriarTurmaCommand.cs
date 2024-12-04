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
    public Cor Cor { get; private set; }
    public Icones Icone { get; private set; }

    public CriarTurmaCommand(
        Usuario professor, 
        string nome, 
        string descricao, 
        string materia, 
        Cor cor,
        Icones icone)
    {
        Id = Guid.NewGuid();
        ValidarParametros(professor, nome, materia);
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        Cor = cor;
        Icone = icone;
    }

    private void ValidarParametros(Usuario professor, string nome, string materia)
    {
        if (professor == null)
            throw new ArgumentException("Professor não pode ser nulo", nameof(professor));
            
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio", nameof(nome));
            
        if (string.IsNullOrWhiteSpace(materia))
            throw new ArgumentException("Matéria não pode ser vazia", nameof(materia));
    }
}