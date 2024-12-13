using EstartandoDevsCore.Messages;
using FluentValidation;

namespace Kognito.Tarefas.App.Commands;

public class EntregarTarefaCommand : Command
{
    public string Conteudo { get; private set; }
    public Guid AlunoId { get; private set; }
    public Guid TarefaId { get; private set; }

    public EntregarTarefaCommand(string conteudo, Guid alunoId, Guid tarefaId)
    {
        Conteudo = conteudo;
        AlunoId = alunoId;
        TarefaId = tarefaId;
    }

    public override bool EstaValido()
    {
        ValidationResult = new EntregarTarefaValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class EntregarTarefaValidation : AbstractValidator<EntregarTarefaCommand>
    {
        public EntregarTarefaValidation()
        {
            RuleFor(x => x.Conteudo)
                .NotEmpty().WithMessage("O conteúdo da entrega é obrigatório");

            RuleFor(x => x.AlunoId)
                .NotEmpty().WithMessage("O ID do aluno é obrigatório");

            RuleFor(x => x.TarefaId)
                .NotEmpty().WithMessage("O ID da tarefa é obrigatório");
        }
    }
}