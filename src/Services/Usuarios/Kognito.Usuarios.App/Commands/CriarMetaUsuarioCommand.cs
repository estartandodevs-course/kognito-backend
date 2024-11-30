using EstartandoDevsCore.Messages;
using FluentValidation;

namespace Kognito.Usuarios.App.Commands;

public class CriarMetaCommand : Command
{
    public string Titulo { get; private set; }
    public string Descricao { get; private set; }

    public CriarMetaCommand(string titulo, string descricao)
    {
        Titulo = titulo;
        Descricao = descricao;
    }

    public override bool EstaValido()
    {
        ValidationResult = new CriarMetaValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class CriarMetaValidation : AbstractValidator<CriarMetaCommand>
    {
        public CriarMetaValidation()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("O título da meta é obrigatório")
                .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição da meta é obrigatória")
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres");
        }
    }
}