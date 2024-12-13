using EstartandoDevsCore.Messages;
using FluentValidation;
using Kognito.Usuarios.App.Domain;

namespace Kognito.Tarefas.App.Commands;

public class CriarTarefaCommand : Command
{
    public Guid Id { get; private set; }
    public string Descricao { get; private set; }
    public string Conteudo { get; private set; }
    public DateTime DataFinalEntrega { get; private set; }
    public Guid TurmaId { get; private set; }
    public List<Neurodivergencia> NeurodivergenciasAlvo { get; private set; }

    public CriarTarefaCommand(Guid id, string descricao, string conteudo, DateTime dataFinalEntrega, Guid turmaId, List<Neurodivergencia>? neurodivergenciasAlvo = null)
    {
        Id = id;
        Descricao = descricao;
        Conteudo = conteudo;
        DataFinalEntrega = dataFinalEntrega;
        TurmaId = turmaId;
        NeurodivergenciasAlvo = neurodivergenciasAlvo ?? new List<Neurodivergencia>();
    }

    public override bool EstaValido()
    {
        ValidationResult = new CriarTarefaValidation().Validate(this);
        return ValidationResult.IsValid;
    }

    public class CriarTarefaValidation : AbstractValidator<CriarTarefaCommand>
    {
        public CriarTarefaValidation()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("A descrição da tarefa é obrigatória")
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres");

            RuleFor(x => x.Conteudo)
                .NotEmpty().WithMessage("O conteúdo da tarefa é obrigatório");

            RuleFor(x => x.DataFinalEntrega)
                .NotEmpty().WithMessage("A data final de entrega é obrigatória")
                .GreaterThan(DateTime.Now).WithMessage("A data final de entrega deve ser maior que a data atual");

            RuleFor(x => x.TurmaId)
                .NotEmpty().WithMessage("O ID da turma é obrigatório");
        }
    }
}