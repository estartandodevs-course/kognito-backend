using System;
using System.Security.Claims;
using EstartandoDevsCore.Mediator;
using EstartandoDevsWebApiCore.Controllers;
using Kognito.Turmas.App.Commands;
using Kognito.Turmas.App.Queries;
using Kognito.Turmas.Domain;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Kognito.Turmas.App.ViewModels;


namespace Kognito.WebApi.Controllers;
// TODO: Será substituído pela obtenção do ID via JWT quando implementado
// ID fixo para testes
[Route("api/turmas")]
public class TurmasController : MainController
{
    private readonly ITurmaQueries _turmaQueries;
    private readonly IMediatorHandler _mediatorHandler;

    public TurmasController(ITurmaQueries turmaQueries, IMediatorHandler mediatorHandler)
    {
        _turmaQueries = turmaQueries;
        _mediatorHandler = mediatorHandler;
    }
    

   private async Task<Guid?> ObterUsuarioIdPorIdentityId()
{
    #if DEBUG
        return new Guid("11111111-1111-1111-1111-111111111111"); // Guid fixo para testes
    #else
        var identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(identityId)) return null;

        var usuario = await _usuarioQueries.ObterPorEmail(User.FindFirst(ClaimTypes.Email)?.Value);
        return usuario?.Id;
    #endif
}

    [HttpGet("professor/{professorId}")]
    public async Task<IActionResult> ObterTurmasProfessor(Guid professorId)
    {
        var turmas = await _turmaQueries.ObterTurmasPorProfessor(professorId);
        return CustomResponse(turmas);
    }

    [HttpGet("aluno/{alunoId}")]
    public async Task<IActionResult> ObterTurmasAluno(Guid alunoId)
    {
        var turmas = await _turmaQueries.ObterTurmasPorAluno(alunoId);
        return CustomResponse(turmas);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var turma = await _turmaQueries.ObterPorId(id);
        return CustomResponse(turma);
    }

    [HttpGet("{turmaId:guid}/alunos/quantidade")]
    public async Task<IActionResult> ObterQuantidadeAlunos(Guid turmaId)
    {
        var quantidade = await _turmaQueries.ObterQuantidadeAlunos(turmaId);
        return CustomResponse(new { QuantidadeAlunos = quantidade });
    }

    [HttpGet("{turmaId:guid}/acesso")]
    public async Task<IActionResult> ObterAcessoTurma(Guid turmaId)
    {
        var acesso = await _turmaQueries.ObterAcessoTurma(turmaId);
        return CustomResponse(acesso);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] TurmaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = await ObterUsuarioIdPorIdentityId();
        if (!usuarioId.HasValue) return Unauthorized();

        var command = new CriarTurmaCommand(
            professor: new Usuario("Professor", usuarioId.Value),
            nome: model.Name,
            descricao: model.Description,
            materia: model.Subject,
            cor: model.Color,
            icone: model.Icon
        );

        var result = await _mediatorHandler.EnviarComando(command);
        if (result.IsValid)
            return CustomResponse("Turma criada com sucesso!");
        return CustomResponse(result);
    }

[HttpPost("ingressar/{hashAcesso}")]
public async Task<IActionResult> IngressarTurma(string hashAcesso, [FromBody] IngressoTurmaInputModel model)
{
    var turma = await _turmaQueries.ObterPorHashAcesso(hashAcesso);
    if (turma == null)
    {
        AdicionarErro("Turma não encontrada");
        return CustomResponse();
    }

    if (turma.Professor.Id == model.StudentId)
    {
        AdicionarErro("Professor não pode ingressar como aluno em sua própria turma");
        return CustomResponse();
    }

    var command = new CriarAlunoNaTurmaCommand(
        id: Guid.NewGuid(),
        alunoId: model.StudentId,
        alunoNome: model.StudentName,
        turmaId: turma.Id,
        status: Enturmamento.EnturtamentoStatus.Ativo
    );

    var result = await _mediatorHandler.EnviarComando(command);
    
    if (result.IsValid)
        return CustomResponse(new 
        { 
            mensagem = "Ingresso na turma realizado com sucesso!",
            turma = turma
        });
        
    return CustomResponse(result);
}

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] TurmaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = await ObterUsuarioIdPorIdentityId();
        if (!usuarioId.HasValue) return Unauthorized();

        var command = new AtualizarTurmaCommand(
            id: id,
            professor: new Usuario("Professor", usuarioId.Value),
            nome: model.Name,
            descricao: model.Description,
            materia: model.Subject
        );

        var result = await _mediatorHandler.EnviarComando(command);
        if (result.IsValid)
        {
            var turmaAtualizada = await _turmaQueries.ObterPorId(id);
            return CustomResponse(turmaAtualizada);
        }
        
        return CustomResponse(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var command = new ExcluirTurmaCommand(id);
        var result = await _mediatorHandler.EnviarComando(command);
        
        if (result.IsValid)
            return CustomResponse("Turma excluída com sucesso!");
            
        return CustomResponse(result);
    }

    [HttpPost("{turmaId:guid}/vincular-conteudo/{conteudoId:guid}")]
    public async Task<IActionResult> VincularConteudo(Guid turmaId, Guid conteudoId)
    {
        var vinculado = await _turmaQueries.VincularTurma(conteudoId, turmaId);
        if (vinculado)
            return CustomResponse("Conteúdo vinculado com sucesso!");
            
        AdicionarErro("Não foi possível vincular o conteúdo à turma");
        return CustomResponse();
    }
}