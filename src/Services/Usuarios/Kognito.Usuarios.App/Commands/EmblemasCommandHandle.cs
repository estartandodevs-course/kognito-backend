using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Kognito.Usuarios.App.Domain.Interface;
using MediatR;

namespace Kognito.Usuarios.App.Commands;

public class EmblemasCommandHandler : IRequestHandler<DesbloquearEmblemaCommand, ValidationResult>
{
    private readonly IEmblemaRepository _emblemaRepository;

    public EmblemasCommandHandler(IEmblemaRepository emblemaRepository)
    {
        _emblemaRepository = emblemaRepository;
    }

    public async Task<ValidationResult> Handle(DesbloquearEmblemaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EhValido()) return request.ValidationResult;

        try
        {
            if (request.QuantidadeEntregas % 3 == 0)
            {
                var proximoEmblema = await _emblemaRepository
                    .ObterProximoEmblemaDisponivel(request.UsuarioId);
                
                if (proximoEmblema != null)
                {
                    proximoEmblema.Desbloquear();
                    await _emblemaRepository.UnitOfWork.Commit();
                }
            }
            
            return new ValidationResult();
        }
        catch (Exception ex)
        {
            return new ValidationResult(new[] 
            { 
                new ValidationFailure("Erro", "Ocorreu um erro ao desbloquear o emblema") 
            });
        }
    }
}