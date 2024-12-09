using EstartandoDevsCore.Messages;
using FluentValidation.Results;

namespace Kognito.Usuarios.App.Commands;

public class DesbloquearEmblemaCommand : Command
{
    public Guid UsuarioId { get; private set; }
    public int QuantidadeEntregas { get; private set; }

    public DesbloquearEmblemaCommand(Guid usuarioId, int quantidadeEntregas)
    {
        UsuarioId = usuarioId;
        QuantidadeEntregas = quantidadeEntregas;
    }

    public bool EhValido()
    {
        ValidationResult = new ValidationResult();
        
        if (UsuarioId == Guid.Empty)
            ValidationResult.Errors.Add(new ValidationFailure("UsuarioId", "ID do usuário inválido"));

        if (QuantidadeEntregas < 0)
            ValidationResult.Errors.Add(new ValidationFailure("QuantidadeEntregas", "Quantidade de entregas inválida"));

        return ValidationResult.IsValid;
    }
}