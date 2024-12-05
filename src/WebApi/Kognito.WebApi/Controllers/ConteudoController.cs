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

 

  [HttpGet("turma/{turmaId:guid}")]
public async Task<IActionResult> ObterPorTurma(Guid turmaId)
{
    try
    {
        var conteudos = await _conteudoQueries.ObterPorTurma(turmaId);
        return CustomResponse(new
        {
            mensagem = "Conteúdos obtidos com sucesso",
            conteudos = conteudos
        });
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
            id: Guid.NewGuid(),
            titulo: model.Title,
            conteudoDidatico: model.Description,
            turmaId: model.ClassId
        );

        var result = await _mediatorHandler.EnviarComando(command);
        if (result.IsValid)
        {
            var conteudoCriado = await _conteudoQueries.ObterPorId(command.Id);
            return CustomResponse(new 
            { 
                mensagem = "Conteúdo criado com sucesso!",
                conteudo = conteudoCriado 
            });
        }
        
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
        if (result.IsValid)
        {
            var conteudoAtualizado = await _conteudoQueries.ObterPorId(id);
            return CustomResponse(new 
            { 
                mensagem = "Conteúdo atualizado com sucesso!",
                conteudo = conteudoAtualizado 
            });
        }
        
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
        var conteudo = await _conteudoQueries.ObterPorId(id);
        if (conteudo == null)
        {
            AdicionarErro("Conteúdo não encontrado");
            return CustomResponse();
        }

        var command = new ExcluirConteudoCommand(id);
        var result = await _mediatorHandler.EnviarComando(command);
        
        if (result.IsValid)
            return CustomResponse(new 
            { 
                mensagem = "Conteúdo excluído com sucesso!",
                conteudoExcluido = conteudo 
            });
            
        return CustomResponse(result);
    }
    catch (Exception ex)
    {
        AdicionarErro($"Erro ao excluir conteúdo: {ex.Message}");
        return CustomResponse();
    }
}
}