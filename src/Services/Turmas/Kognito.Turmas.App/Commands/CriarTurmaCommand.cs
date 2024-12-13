using EstartandoDevsCore.Messages;
using FluentValidation.Results;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Common;

public class CriarTurmaCommand : Command
{
    public Guid ProfessorId { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public Cor Cor { get; private set; }
    public Icones Icone { get; private set; }

    public CriarTurmaCommand(
        Guid professorId, 
        string nome, 
        string descricao, 
        string materia, 
        Cor cor,
        Icones icone)
    {
        var validationResult = ValidarParametros(professorId, nome, materia);
        if (validationResult.Success)
        {
            ProfessorId = professorId;
            Nome = nome;
            Descricao = descricao;
            Materia = materia;
            Cor = cor;
            Icone = icone;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarParametros(Guid professorId, string nome, string materia)
    {
        var errors = new List<string>();

        if (professorId == Guid.Empty)
            errors.Add("Id do professor não pode ser vazio");
            
        if (string.IsNullOrWhiteSpace(nome))
            errors.Add("Nome não pode ser vazio");
            
        if (string.IsNullOrWhiteSpace(materia))
            errors.Add("Matéria não pode ser vazia");

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}