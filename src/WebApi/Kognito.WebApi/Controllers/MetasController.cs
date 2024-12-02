using EstartandoDevsCore.Mediator;
using Kognito.Usuarios.App.Commands;
using Kognito.Usuarios.App.Queries;
using EstartandoDevsWebApiCore.Controllers;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Kognito.WebApi.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> ObterMetasDoUsuario()
    {
        var usuarioId = await ObterUsuarioIdPorIdentityId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var metas = await _usuarioQueries.ObterMetas(usuarioId.Value);
        return CustomResponse(metas);
    }

    [HttpPost]
    public async Task<IActionResult> CriarMeta([FromBody] MetaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = await ObterUsuarioIdPorIdentityId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var command = new CriarMetaCommand(usuarioId.Value, model.Title, model.Description);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse(result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarMeta(Guid id, [FromBody] MetaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = await ObterUsuarioIdPorIdentityId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var command = new AtualizarMetaCommand(id, model.Title, model.Description);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> RemoverMeta(Guid id)
    {
        var usuarioId = await ObterUsuarioIdPorIdentityId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var command = new RemoverMetaCommand(id);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse(result);
    }

    [HttpPut("{id:guid}/concluir")]
    public async Task<IActionResult> ConcluirMeta(Guid id)
    {
        var usuarioId = await ObterUsuarioIdPorIdentityId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var command = new ConcluirMetaCommand(id);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse(result);
    }
}