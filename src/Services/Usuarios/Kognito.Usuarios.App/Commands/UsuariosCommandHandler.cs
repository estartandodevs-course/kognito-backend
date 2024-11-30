﻿using EstartandoDevsCore.Messages;
using Kognito.Usuarios.App.Domain;
using Kognito.Usuarios.App.Domain.Interface;
using EstartandoDevsCore.ValueObjects;
using FluentValidation.Results;
using MediatR;

namespace Kognito.Usuarios.App.Commands;

public class UsuariosCommandHandler : CommandHandler,
    IRequestHandler<CriarUsuarioCommand, ValidationResult>,
    IRequestHandler<AtualizarUsuarioCommand, ValidationResult>,
    IRequestHandler<CriarMetaCommand, ValidationResult>,
    IRequestHandler<AtualizarMetaCommand, ValidationResult>,
    IRequestHandler<RemoverMetaCommand, ValidationResult>,
    IRequestHandler<ConcluirMetaCommand, ValidationResult>,
    IDisposable
{
    private readonly IUsuariosRepository _usuarioRepository;

    public UsuariosCommandHandler(IUsuariosRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<ValidationResult> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        var usuario = new Usuario(request.Nome, new Cpf(request.Cpf), (Neurodivergencia)Enum.Parse(typeof(Neurodivergencia), request.Neurodivergencia));
        
        _usuarioRepository.Adicionar(usuario);
        
        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;

        var usuario = await _usuarioRepository.ObterPorId(request.UsuarioId);
        if (usuario is null)
        {
            AdicionarErro("Usuário não encontrado!");
            return ValidationResult;
        }

        usuario.AtribuirNome(request.Nome);
        usuario.AtribuirNeurodivergencia((Neurodivergencia)Enum.Parse(typeof(Neurodivergencia), request.Neurodivergencia));
        
        _usuarioRepository.Atualizar(usuario);
        
        return await PersistirDados(_usuarioRepository.UnitOfWork);
    }

    public async Task<ValidationResult> Handle(CriarMetaCommand request, CancellationToken cancellationToken)
    {
        if (!request.EstaValido()) return request.ValidationResult;
        
        var meta = new Metas(request.Titulo, request.Descricao);
        
        _usuarioRepository.AdicionarMeta(meta);
        
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

    public void Dispose()
    {
        _usuarioRepository?.Dispose();
    }
}