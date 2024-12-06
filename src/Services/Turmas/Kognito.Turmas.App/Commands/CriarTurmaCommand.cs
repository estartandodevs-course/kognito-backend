using EstartandoDevsCore.Messages;
using FluentValidation.Results;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Common;

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
        var validationResult = ValidarParametros(professor, nome, materia);
        if (validationResult.Success)
        {
            Professor = professor;
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

    private Result ValidarParametros(Usuario professor, string nome, string materia)
    {
        var errors = new List<string>();

        if (professor == null)
            errors.Add("Professor não pode ser vazio");
            
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