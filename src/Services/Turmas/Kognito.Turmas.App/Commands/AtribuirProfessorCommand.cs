using EstartandoDevsCore.Messages;
using FluentValidation;

public class AtribuirProfessorCommand : Command
{
    public Guid TurmaId { get; set; }
    public Guid ProfessorId { get; set; }
    
    public AtribuirProfessorCommand(Guid turmaId, Guid professorId)
    {
        ValidarIds(turmaId, professorId);
        TurmaId = turmaId;
        ProfessorId = professorId;
    }

    private void ValidarIds(Guid turmaId, Guid professorId)
    {
        if (turmaId == Guid.Empty)
            throw new ArgumentException("Id da turma inválido", nameof(turmaId));
            
        if (professorId == Guid.Empty)
            throw new ArgumentException("Id do professor inválido", nameof(professorId));
    }

    // Adicionar método EstaValido
    public override bool EstaValido()
    {
        ValidationResult = new AtribuirProfessorValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class AtribuirProfessorValidation : AbstractValidator<AtribuirProfessorCommand>
    {
        public AtribuirProfessorValidation()
        {
            RuleFor(x => x.TurmaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da turma é inválido");

            RuleFor(x => x.ProfessorId)
                .NotEqual(Guid.Empty).WithMessage("O ID do professor é inválido");
        }
    }
}