using EstartandoDevsCore.Messages;
using FluentValidation;

namespace Kognito.Turmas.App.Commands;

public class GerarLinkTurmaCommand : Command
{
    public Guid TurmaId { get; private set; }
    public int? LimiteUsos { get; private set; }
    public int? DiasValidade { get; private set; }

    public GerarLinkTurmaCommand(Guid turmaId, int? limiteUsos = null, int? diasValidade = null)
    {
        TurmaId = turmaId;
        LimiteUsos = limiteUsos;
        DiasValidade = diasValidade;
    }

    public override bool EstaValido()
    {
        ValidationResult = new GerarLinkTurmaValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class GerarLinkTurmaValidation : AbstractValidator<GerarLinkTurmaCommand>
    {
        public GerarLinkTurmaValidation()
        {
            RuleFor(x => x.TurmaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da turma é inválido");

            When(x => x.LimiteUsos.HasValue, () =>
            {
                RuleFor(x => x.LimiteUsos.Value)
                    .GreaterThan(0).WithMessage("O limite de usos deve ser maior que zero");
            });

            When(x => x.DiasValidade.HasValue, () =>
            {
                RuleFor(x => x.DiasValidade.Value)
                    .GreaterThan(0).WithMessage("O número de dias de validade deve ser maior que zero");
            });
        }
    }
}