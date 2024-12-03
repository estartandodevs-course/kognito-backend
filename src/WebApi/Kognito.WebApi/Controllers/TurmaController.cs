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

    private Guid? ObterProfessorId()
    {
        #if DEBUG
            return Guid.NewGuid(); // ID fixo para testes
        #else
            var professorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return string.IsNullOrEmpty(professorId) ? null : Guid.Parse(professorId);
        #endif
    }

    private Usuario ObterProfessor()
    {
        var professorId = ObterProfessorId();
        if (!professorId.HasValue)
            throw new ArgumentException("Professor não encontrado");

        #if DEBUG
            return new Usuario("Professor Teste", professorId.Value);
        #else
            return new Usuario("Professor", professorId.Value);
        #endif
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var turmas = await _turmaQueries.ObterTodasTurmas();
        return CustomResponse(turmas);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var turma = await _turmaQueries.ObterPorId(id);
        return CustomResponse(turma);
    }

    [HttpGet("minhas-turmas")]
    public async Task<IActionResult> ObterMinhasTurmas()
    {
        try
        {
            var professorId = ObterProfessorId();
            if (!professorId.HasValue)
            {
                AdicionarErro("Professor não encontrado");
                return CustomResponse();
            }

            var turmas = await _turmaQueries.ObterTurmasPorProfessor(professorId.Value);

            if (!turmas.Any())
            {
                return CustomResponse(new
                {
                    Mensagem = "Nenhuma turma encontrada",
                    ProfessorId = professorId.Value,
                    Turmas = new List<TurmaViewModel>()
                });
            }

            return CustomResponse(new
            {
                ProfessorId = professorId.Value,
                Turmas = turmas
            });
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao obter turmas do professor: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpGet("{turmaId:guid}/alunos/quantidade")]
    public async Task<IActionResult> ObterQuantidadeAlunos(Guid turmaId)
    {
        try
        {
            var quantidade = await _turmaQueries.ObterQuantidadeAlunos(turmaId);
            return CustomResponse(new { QuantidadeAlunos = quantidade });
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao obter quantidade de alunos: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpGet("{turmaId:guid}/acesso")]
    public async Task<IActionResult> ObterAcessoTurma(Guid turmaId)
    {
        try
        {
            var acesso = await _turmaQueries.ObterAcessoTurma(turmaId);
            return CustomResponse(acesso);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao obter informações de acesso: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpGet("{turmaId:guid}/validar-hash/{hash}")]
    public async Task<IActionResult> ValidarHashAcesso(Guid turmaId, string hash)
    {
        try
        {
            var hashValido = await _turmaQueries.ValidarHashAcesso(turmaId, hash);
            return CustomResponse(new { HashValido = hashValido });
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao validar hash de acesso: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] TurmaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        try
        {
            var professor = ObterProfessor();
            var turmaId = Guid.NewGuid();

            var command = new CriarTurmaCommand(
                id: turmaId,
                professor: professor,
                nome: model.Name,           
                descricao: model.Description, 
                materia: model.Subject,     
                cor: model.Color,           
                icone: model.Icon           
            );

            var result = await _mediatorHandler.EnviarComando(command);
            
            if (result.IsValid)
            {
                var turma = await _turmaQueries.ObterAcessoTurma(turmaId);
                
                return CustomResponse(new
                {
                    Id = turmaId,
                    Nome = model.Name,
                    Descricao = model.Description,
                    Materia = model.Subject,
                    Cor = model.Color,
                    Icones = model.Icon,
                    DataDeCadastro = DateTime.Now,
                    Professor = professor.Nome,
                    HashAcesso = turma?.HashAcesso ?? string.Empty,
                    LinkAcesso = turma?.LinkAcesso ?? string.Empty
                });
            }
            
            foreach (var erro in result.Errors)
            {
                AdicionarErro(erro.ErrorMessage);
            }
            
            return CustomResponse();
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao criar turma: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] TurmaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        try
        {
            var professor = ObterProfessor();

            var command = new AtualizarTurmaCommand(
                id: id,
                professor: professor,
                nome: model.Name,           
                descricao: model.Description, 
                materia: model.Subject
            );

            var result = await _mediatorHandler.EnviarComando(command);
            
            if (result.IsValid)
            {
                return CustomResponse(new
                {
                    Id = id,
                    Nome = model.Name,
                    Descricao = model.Description,
                    Materia = model.Subject,
                    Professor = professor.Nome
                });
            }
            
            foreach (var erro in result.Errors)
            {
                AdicionarErro(erro.ErrorMessage);
            }
            
            return CustomResponse();
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao atualizar turma: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        try
        {
            var command = new ExcluirTurmaCommand(id);
            var result = await _mediatorHandler.EnviarComando(command);

            if (result.IsValid)
            {
                return CustomResponse(new
                {
                    Mensagem = "Turma excluída com sucesso",
                    TurmaId = id,
                    DataExclusao = DateTime.Now
                });
            }

            foreach (var erro in result.Errors)
            {
                AdicionarErro(erro.ErrorMessage);
            }

            return CustomResponse();
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao excluir turma: {ex.Message}");
            return CustomResponse();
        }
    }
}