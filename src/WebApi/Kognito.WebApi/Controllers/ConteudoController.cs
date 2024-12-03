using EstartandoDevsCore.Mediator;
using EstartandoDevsWebApiCore.Controllers;
using Kognito.Turmas.App.Commands;
using Kognito.Turmas.App.Queries;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kognito.WebApi.Controllers;


[Route("api/conteudos")]
public class ConteudosController : MainController
{
    private readonly IConteudoQueries _conteudoQueries;
    private readonly IMediatorHandler _mediatorHandler;

    public ConteudosController(IConteudoQueries conteudoQueries, IMediatorHandler mediatorHandler)
    {
        _conteudoQueries = conteudoQueries;
        _mediatorHandler = mediatorHandler;
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        try
        {
            var conteudos = await _conteudoQueries.ObterTodosConteudos();
            return CustomResponse(conteudos);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao obter conteúdos: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpGet("turma/{turmaId:guid}")]
    public async Task<IActionResult> ObterPorTurma(Guid turmaId)
    {
        try
        {
            var conteudos = await _conteudoQueries.ObterPorTurma(turmaId);
            return CustomResponse(conteudos);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao obter conteúdos da turma: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpGet("turma/{turmaId:guid}/quantidade")]
    public async Task<IActionResult> ObterQuantidadeConteudos(Guid turmaId)
    {
        try
        {
            var quantidade = await _conteudoQueries.ObterQuantidadeConteudosPorTurma(turmaId);
            return CustomResponse(new { QuantidadeConteudos = quantidade });
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao obter quantidade de conteúdos: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ConteudoInputModel model)
    {
        try
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var command = new CriarConteudoCommand(
                Guid.NewGuid(),
                model.Title,
                model.Description
            );

            var result = await _mediatorHandler.EnviarComando(command);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao criar conteúdo: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] ConteudoInputModel model)
    {
        try
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var command = new AtualizarConteudoCommand(
                id,
                model.Title,
                model.Description,
                model.ClassId
            );

            var result = await _mediatorHandler.EnviarComando(command);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao atualizar conteúdo: {ex.Message}");
            return CustomResponse();
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        try
        {
            var command = new ExcluirConteudoCommand(id);
            var result = await _mediatorHandler.EnviarComando(command);
            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao excluir conteúdo: {ex.Message}");
            return CustomResponse();
        }
    }
    
}