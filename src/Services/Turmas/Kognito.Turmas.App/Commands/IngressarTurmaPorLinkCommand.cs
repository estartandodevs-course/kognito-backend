using EstartandoDevsCore.Messages;
using FluentValidation;

namespace Kognito.Turmas.App.Commands;

public class IngressarTurmaPorLinkCommand : Command
{
    public Guid TurmaId { get; private set; }
    public string Codigo { get; private set; }
    public Guid AlunoId { get; private set; }

    public IngressarTurmaPorLinkCommand(Guid turmaId, string codigo, Guid alunoId)
    {
        TurmaId = turmaId;
        Codigo = codigo;
        AlunoId = alunoId;
    }

    public override bool EstaValido()
    {
        ValidationResult = new IngressarTurmaPorLinkValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class IngressarTurmaPorLinkValidation : AbstractValidator<IngressarTurmaPorLinkCommand>
    {
        public IngressarTurmaPorLinkValidation()
        {
            RuleFor(x => x.TurmaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da turma é inválido");

            RuleFor(x => x.Codigo)
                .NotEmpty().WithMessage("O código do link é obrigatório")
                .MaximumLength(50).WithMessage("O código deve ter no máximo 50 caracteres");

            RuleFor(x => x.AlunoId)
                .NotEqual(Guid.Empty).WithMessage("O ID do aluno é inválido");
        }
    }
}