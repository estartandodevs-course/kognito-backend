using System;
using EstartandoDevsCore.Messages;
using Kognito.Turmas.Domain.Common;
using FluentValidation.Results;

namespace Kognito.Turmas.App.Commands;

public class AtualizarConteudoCommand : Command
{
    public Guid ConteudoId { get; private set; }
    public string Titulo { get; private set; }
    public string ConteudoDidatico { get; private set; }

    public AtualizarConteudoCommand(Guid conteudoId, string titulo, string conteudoDidatico, Guid classId)
    {
        var validationResult = ValidarParametros(conteudoId, titulo);
        if (validationResult.Success)
        {
            ConteudoId = conteudoId;
            Titulo = titulo;
            ConteudoDidatico = conteudoDidatico;
        }
        else
        {
            foreach (var error in validationResult.Errors)
                ValidationResult.Errors.Add(new ValidationFailure(string.Empty, error));
        }
    }

    private Result ValidarParametros(Guid id, string titulo)
    {
        var errors = new List<string>();

        if (id == Guid.Empty)
            errors.Add("Id do conteúdo inválido");
            
        if (string.IsNullOrWhiteSpace(titulo))
            errors.Add("Título não pode ser vazio");

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

    public override bool EstaValido()
    {
        return ValidationResult.IsValid;
    }
}