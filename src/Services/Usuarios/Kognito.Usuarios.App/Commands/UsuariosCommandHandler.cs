using EstartandoDevsCore.Messages;
using Kognito.Usuarios.App.Domain;
using Kognito.Usuarios.App.Domain.Interface;
using EstartandoDevsCore.ValueObjects;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using MediatR;

namespace Kognito.Usuarios.App.Commands;

public class UsuariosCommandHandler : CommandHandler,
    IRequestHandler<CriarProfessorCommand, ValidationResult>,
    IRequestHandler<CriarAlunoCommand, ValidationResult>,
    IRequestHandler<CriarUsuarioCommand, ValidationResult>,
    IRequestHandler<AtualizarUsuarioCommand, ValidationResult>,
    IRequestHandler<CriarMetaCommand, ValidationResult>,
    IRequestHandler<AdicionarNeurodivergenciaCommand, ValidationResult>,
    IRequestHandler<AtualizarMetaCommand, ValidationResult>,
    IRequestHandler<RemoverMetaCommand, ValidationResult>,
    IRequestHandler<ConcluirMetaCommand, ValidationResult>,
    
    IDisposable
{
    private readonly IUsuariosRepository _usuarioRepository;
    private readonly UserManager<IdentityUser> _userManager;

    public UsuariosCommandHandler(
        IUsuariosRepository usuarioRepository,
        UserManager<IdentityUser> userManager)
    {
        _usuarioRepository = usuarioRepository;
        _userManager = userManager;
    }

    public async Task<ValidationResult> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        var cpf = new Cpf(request.Cpf);
        if (!cpf.EstaValido())
        {
            AdicionarErro("CPF inválido");
            return ValidationResult;
        }

        var usuarioExistente = await _usuarioRepository.ObterPorCpf(request.Cpf);
        if (usuarioExistente != null)
        {
            AdicionarErro("CPF já está em uso");
            return ValidationResult;
        }
    
        if (string.IsNullOrEmpty(request.Nome))
        {
            AdicionarErro("O nome do usuário é obrigatório");
            return ValidationResult;
        }

        Neurodivergencia? neurodivergencia = null;
        if (!string.IsNullOrEmpty(request.Neurodivergencia))
        {
            if (!Enum.TryParse<Neurodivergencia>(request.Neurodivergencia, out var parsedNeurodivergencia))
            {
                AdicionarErro("Neurodivergência informada é inválida");
                return ValidationResult;
            }
            neurodivergencia = parsedNeurodivergencia;
        }

        var usuario = new Usuario(request.Nome, cpf, neurodivergencia);
        var login = new Login(new Email(request.Email), new Senha(request.Senha));
        usuario.AtribuirLogin(login);

        _usuarioRepository.Adicionar(usuario);

        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.ObterPorId(request.UsuarioId);
        if (usuario is null)
        {
            AdicionarErro("Usuário não encontrado!");
            return ValidationResult;
        }

        var identityUser = await _userManager.FindByEmailAsync(usuario.Login.Email.Endereco);
        if (identityUser is null)
        {
            AdicionarErro("Usuário de identidade não encontrado!");
            return ValidationResult;
        }

        usuario.AtribuirNome(request.Nome);
        var login = new Login(new Email(request.Email), usuario.Login.Senha);
        usuario.AtribuirLogin(login);

        identityUser.Email = request.Email;
        identityUser.NormalizedEmail = request.Email.ToUpper();
        identityUser.UserName = request.Email;
        identityUser.NormalizedUserName = request.Email.ToUpper();

        var identityResult = await _userManager.UpdateAsync(identityUser);
        if (!identityResult.Succeeded)
        {
            foreach (var erro in identityResult.Errors)
                AdicionarErro(erro.Description);
            return ValidationResult;
        }

        _usuarioRepository.Atualizar(usuario);

        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(CriarMetaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;
    
        var usuario = await _usuarioRepository.ObterPorId(request.UsuarioId);
        if (usuario == null)
        {
            AdicionarErro("Usuário não encontrado");
            return ValidationResult;
        }
    
        var meta = new Metas(request.Titulo, request.Descricao);
        usuario.AdicionarMeta(meta);
    
        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }
    
    
    public async Task<ValidationResult> Handle(AtualizarMetaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;
        
        var meta = await _usuarioRepository.ObterMetaPorId(request.MetaId);
        if (meta is null)
        {
            AdicionarErro("Meta não encontrada!");
            return ValidationResult;
        }

        meta.AtribuirTitulo(request.Titulo);
        meta.AtribuirDescricao(request.Descricao);
        
        _usuarioRepository.AtualizarMeta(meta);
        
        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(RemoverMetaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;
        
        var meta = await _usuarioRepository.ObterMetaPorId(request.MetaId);
        if (meta is null)
        {
            AdicionarErro("Meta não encontrada!");
            return ValidationResult;
        }

        _usuarioRepository.RemoverMeta(meta);
        
        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(ConcluirMetaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;
        
        var meta = await _usuarioRepository.ObterMetaPorId(request.MetaId);
        if (meta is null)
        {
            AdicionarErro("Meta não encontrada!");
            return ValidationResult;
        }

        meta.Concluir();
        _usuarioRepository.AtualizarMeta(meta);
        
        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }
    
    public async Task<ValidationResult> Handle(CriarProfessorCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        var cpf = new Cpf(request.Cpf);
        if (!cpf.EstaValido())
        {
            AdicionarErro("CPF inválido");
            return ValidationResult;
        }

        var usuarioExistente = await _usuarioRepository.ObterPorCpf(request.Cpf);
        if (usuarioExistente != null)
        {
            AdicionarErro("CPF já está em uso");
            return ValidationResult;
        }

        var usuario = new Usuario(request.Nome, cpf);
        var login = new Login(new Email(request.Email), new Senha(request.Senha));
        usuario.AtribuirLogin(login);

        _usuarioRepository.Adicionar(usuario);

        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(CriarAlunoCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        var cpf = new Cpf(request.Cpf);
        if (!cpf.EstaValido())
        {
            AdicionarErro("CPF inválido");
            return ValidationResult;
        }

        var usuarioExistente = await _usuarioRepository.ObterPorCpf(request.Cpf);
        if (usuarioExistente != null)
        {
            AdicionarErro("CPF já está em uso");
            return ValidationResult;
        }

        var usuario = new Usuario(request.Nome, cpf);
        var login = new Login(new Email(request.Email), new Senha(request.Senha));
        usuario.AtribuirLogin(login);
        usuario.AtribuirResponsavelEmail(request.EmailResponsavel);

        _usuarioRepository.Adicionar(usuario);

        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }
    
    public async Task<ValidationResult> Handle(AdicionarNeurodivergenciaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        var usuario = await _usuarioRepository.ObterPorId(request.UsuarioId);
        if (usuario is null)
        {
            AdicionarErro("Usuário não encontrado!");
            return ValidationResult;
        }

        if (usuario.CodigoPai != request.CodigoPai)
        {
            AdicionarErro("Código do responsável inválido!");
            return ValidationResult;
        }

        if (!Enum.TryParse<Neurodivergencia>(request.Neurodivergencia, out var neurodivergencia))
        {
            AdicionarErro("Neurodivergência informada é inválida");
            return ValidationResult;
        }

        usuario.AtribuirNeurodivergencia(neurodivergencia);
        _usuarioRepository.Atualizar(usuario);

        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public void Dispose()
    {
        _usuarioRepository?.Dispose();
    }
}