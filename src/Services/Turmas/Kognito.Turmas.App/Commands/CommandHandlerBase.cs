using FluentValidation.Results;
using EstartandoDevsCore.Data;

namespace Kognito.Turmas.App.Commands;

public abstract class CommandHandlerBase
{
    private object _resultado;
    protected ValidationResult ValidationResult { get; private set; }

    protected CommandHandlerBase()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AdicionarErro(string mensagem)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
    }

    protected void AdicionarResultado<T>(T resultado)
    {
        _resultado = resultado;
    }

    protected T ObterResultado<T>()
    {
        return _resultado != null ? (T)_resultado : default;
    }

    protected async Task<ValidationResult> PersistirDados(IUnitOfWorks uow)
    {
        if (!await uow.Commit()) 
            AdicionarErro("Houve um erro ao persistir os dados");
        
        return ValidationResult;
    }
}