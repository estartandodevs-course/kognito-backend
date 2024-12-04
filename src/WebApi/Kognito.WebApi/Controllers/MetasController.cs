using EstartandoDevsCore.Mediator;
using Kognito.Usuarios.App.Commands;
using Kognito.Usuarios.App.Queries;
using EstartandoDevsWebApiCore.Controllers;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Kognito.Usuarios.App.ViewModels;

namespace Kognito.WebApi.Controllers;

/// <summary>
/// Controller para gerenciamento de metas dos usuários
/// </summary>
[Authorize]
[Route("api/metas")]
public class MetasController : MainController
{
    private readonly IUsuarioQueries _usuarioQueries;
    private readonly IMediatorHandler _mediatorHandler;

    public MetasController(
        IUsuarioQueries usuarioQueries,
        IMediatorHandler mediatorHandler)
    {
        _usuarioQueries = usuarioQueries;
        _mediatorHandler = mediatorHandler;
    }

    private async Task<Guid?> ObterUsuarioIdPorIdentityId()
    {
        var identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(identityId)) return null;

        var usuario = await _usuarioQueries.ObterPorEmail(User.FindFirst(ClaimTypes.Email)?.Value);
        return usuario?.Id;
    }

    private Guid? ObterUsuarioId()
    {
        var userIdClaim = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return null;

        return userId;
    }


    /// <summary>
    /// Obtém todas as metas do usuário autenticado
    /// </summary>
    /// <response code="200">Retorna a lista de metas do usuário</response>
    /// <response code="401">Quando o usuário não está autenticado</response>
    /// <response code="400">Quando a requisição é inválida</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [HttpGet]
    public async Task<IActionResult> ObterMetasDoUsuario()
    {
        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var metas = await _usuarioQueries.ObterMetas(usuarioId.Value);
        return CustomResponse(metas);
    }

    /// <summary>
    /// Cria uma nova meta para o usuário 
    /// </summary>
    /// <param name="model">Informações da meta</param>
    /// <response code="201">Meta criada com sucesso</response>
    /// <response code="400">Quando a requisição é inválida</response>
    /// <response code="401">Quando o usuário não está autenticado</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [HttpPost]
    public async Task<IActionResult> CriarMeta([FromBody] MetaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var command = new CriarMetaCommand(usuarioId.Value, model.Title, model.Description);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse("Meta criada com sucesso!");
    }

    /// <summary>
    /// Atualiza uma meta existente
    /// </summary>
    /// <param name="id">ID da meta</param>
    /// <param name="model">Informações atualizadas da meta</param>
    /// <response code="200">Meta atualizada com sucesso</response>
    /// <response code="400">Quando a requisição é inválida</response>
    /// <response code="401">Quando o usuário não está autenticado</response>
    /// <response code="404">Quando o usuário ou a meta não são encontrados</response>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarMeta(Guid id, [FromBody] MetaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var command = new AtualizarMetaCommand(id, model.Title, model.Description);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse("Meta atualizada com sucesso!");
    }

    /// <summary>
    /// Remove uma meta
    /// </summary>
    /// <param name="id">ID da meta a ser removida</param>
    /// <response code="204">Meta removida com sucesso</response>
    /// <response code="401">Quando o usuário não está autenticado</response>
    /// <response code="404">Quando o usuário ou a meta não são encontrados</response>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoverMeta(Guid id)
    {
        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var command = new RemoverMetaCommand(id);
        var result = await _mediatorHandler.EnviarComando(command);

        return NoContent();
    }

    /// <summary>
    /// Marca uma meta como concluída
    /// </summary>
    /// <param name="id">ID da meta a ser concluída</param>
    /// <response code="200">Meta concluída com sucesso</response>
    /// <response code="401">Quando o usuário não está autenticado</response>
    /// <response code="404">Quando o usuário ou a meta não são encontrados</response>
    [HttpPut("{id:guid}/concluir")]
    public async Task<IActionResult> ConcluirMeta(Guid id)
    {
        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var command = new ConcluirMetaCommand(id);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse("Meta concluída com sucesso!");
    }
}