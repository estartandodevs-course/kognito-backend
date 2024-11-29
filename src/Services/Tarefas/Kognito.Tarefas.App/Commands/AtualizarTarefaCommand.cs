using EstartandoDevsCore.Messages;
using FluentValidation;

namespace Kognito.Tarefas.App.Commands;

public class AtualizarTarefaCommand : Command
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime DataFinalEntrega { get; private set; }
    public Guid TurmaId { get; private set; }

    public AtualizarTarefaCommand(Guid id, string descricao, string conteudo, DateTime dataFinalEntrega, Guid turmaId)
    {
        Id = id;
        Descricao = descricao;
        Conteudo = conteudo;
        DataFinalEntrega = dataFinalEntrega;
        TurmaId = turmaId;
    }

    public override bool EstaValido()
    {
        ValidationResult = new AtualizarTarefaValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class AtualizarTarefaValidation : AbstractValidator<AtualizarTarefaCommand>
    {
        public AtualizarTarefaValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("O ID da tarefa é inválido");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição da tarefa é obrigatória")
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres");

            RuleFor(x => x.Conteudo)
                .NotEmpty().WithMessage("O conteúdo da tarefa é obrigatório");

            RuleFor(x => x.DataFinalEntrega)
                .NotEmpty().WithMessage("A data final de entrega é obrigatória");

            RuleFor(x => x.TurmaId)
                .NotEqual(Guid.Empty).WithMessage("O ID da turma é inválido");
        }
    }
}