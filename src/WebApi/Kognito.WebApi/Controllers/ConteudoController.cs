using EstartandoDevsCore.Mediator;
using EstartandoDevsWebApiCore.Controllers;
using Kognito.Turmas.App.Commands;
using Kognito.Turmas.App.Queries;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kognito.WebApi.Controllers;

[Authorize]
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

    /// <summary>
    /// Obtém todos os conteúdos associados a uma turma específica
    /// </summary>
    /// <param name="turmaId">ID da turma para consulta</param>
    /// <returns>Lista de conteúdos da turma</returns>
    /// <response code="200">Retorna a lista de conteúdos da turma</response>
    /// <response code="400">Quando o ID da turma é inválido</response>
    /// <response code="404">Quando a turma não é encontrada</response>
    /// <response code="500">Quando ocorre um erro interno ao buscar os conteúdos</response>
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


    /// <summary>
    /// Obtém a quantidade total de conteúdos em uma turma
    /// </summary>
    /// <param name="turmaId">ID da turma para consulta</param>
    /// <returns>Quantidade de conteúdos na turma</returns>
    /// <response code="200">Retorna a quantidade de conteúdos</response>
    /// <response code="400">Quando o ID da turma é inválido</response>
    /// <response code="404">Quando a turma não é encontrada</response>
    /// <response code="500">Quando ocorre um erro interno ao contar os conteúdos</response>
    [HttpGet("turma/{turmaId:guid}/quantidade")]
    public async Task<IActionResult> ObterQuantidadeConteudos(Guid turmaId)
    {
        try
        {
            var quantidade = await _conteudoQueries.ObterQuantidadeConteudosPorTurma(turmaId);
            return CustomResponse(new { quantity = quantidade });
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao obter quantidade de conteúdos: {ex.Message}");
            return CustomResponse();
        }
    }

    /// <summary>
    /// Cria um novo conteúdo didático para uma turma
    /// </summary>
    /// <param name="model">Dados do conteúdo contendo título, descrição e ID da turma</param>
    /// <returns>Dados do conteúdo criado</returns>
    /// <response code="200">Conteúdo criado com sucesso, retornando seus dados</response>
    /// <response code="400">Quando os dados da requisição são inválidos</response>
    /// <response code="404">Quando a turma não é encontrada</response>
    /// <response code="500">Quando ocorre um erro interno ao criar o conteúdo</response>
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
                return CustomResponse("Conteúdo criado com sucesso!");
            }

            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao criar conteúdo: {ex.Message}");
            return CustomResponse();
        }
    }

    /// <summary>
    /// Atualiza um conteúdo didático existente
    /// </summary>
    /// <param name="id">ID do conteúdo a ser atualizado</param>
    /// <param name="model">Novos dados do conteúdo</param>
    /// <returns>Dados atualizados do conteúdo</returns>
    /// <response code="200">Conteúdo atualizado com sucesso, retornando dados atualizados</response>
    /// <response code="400">Quando os dados da requisição são inválidos</response>
    /// <response code="404">Quando o conteúdo não é encontrado</response>
    /// <response code="500">Quando ocorre um erro interno ao atualizar o conteúdo</response>
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
                return CustomResponse("Conteúdo atualizado com sucesso!");
            }

            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao atualizar conteúdo: {ex.Message}");
            return CustomResponse();
        }
    }

    /// <summary>
    /// Remove permanentemente um conteúdo didático
    /// </summary>
    /// <param name="id">ID do conteúdo a ser removido</param>
    /// <returns>Confirmação da exclusão com dados do conteúdo excluído</returns>
    /// <response code="200">Conteúdo excluído com sucesso, retornando seus dados</response>
    /// <response code="404">Quando o conteúdo não é encontrado</response>
    /// <response code="500">Quando ocorre um erro interno ao excluir o conteúdo</response>
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
                return CustomResponse("Conteúdo excluído com sucesso!");

            return CustomResponse(result);
        }
        catch (Exception ex)
        {
            AdicionarErro($"Erro ao excluir conteúdo: {ex.Message}");
            return CustomResponse();
        }
    }
}