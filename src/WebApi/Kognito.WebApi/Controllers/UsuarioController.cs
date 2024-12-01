using EstartandoDevsWebApiCore.Controllers;
using EstartandoDevsCore.Mediator;
using EstartandoDevsCore.ValueObjects;
using Kognito.Usuarios.App.Commands;
using Kognito.Usuarios.App.Queries;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace Kognito.Usuarios.App.Controllers;

[Route("api/usuarios")]
public class UsuariosController : MainController
{
    private readonly IUsuarioQueries _usuarioQueries;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly UserManager<IdentityUser> _userManager;

    public UsuariosController(
        IUsuarioQueries usuarioQueries, 
        IMediatorHandler mediatorHandler,
        UserManager<IdentityUser> userManager)
    {
        _usuarioQueries = usuarioQueries;
        _mediatorHandler = mediatorHandler;
        _userManager = userManager;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var usuario = await _usuarioQueries.ObterPorId(id);
        
        if (usuario == null)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        return CustomResponse(usuario);
    }

    [HttpGet("{usuarioId:guid}/emblemas")]
    public async Task<IActionResult> ObterEmblemas(Guid usuarioId)
    {
        var emblemas = await _usuarioQueries.ObterEmblemas(usuarioId);
        return CustomResponse(emblemas);
    }

    [HttpGet("{usuarioId:guid}/metas")]
    public async Task<IActionResult> ObterMetas(Guid usuarioId)
    {
        var metas = await _usuarioQueries.ObterMetas(usuarioId);
        return CustomResponse(metas);
    }

    
    [HttpPost]
    public async Task<IActionResult> CriarUsuario([FromBody] UsuarioInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var user = new IdentityUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = model.Email,
            NormalizedUserName = model.Email.ToUpper(),
            Email = model.Email,
            NormalizedEmail = model.Email.ToUpper(),
            EmailConfirmed = true
        };

        var identidadeCriada = await _userManager.CreateAsync(user, model.Senha);

        if (!identidadeCriada.Succeeded)
        {
            foreach (var erro in identidadeCriada.Errors)
                AdicionarErro(erro.Description);

            return CustomResponse();
        }

        var command = new CriarUsuarioCommand(
            model.Nome,
            model.Cpf,
            model.Neurodivergencia,
            model.Email,
            model.Senha
        );

        var result = await _mediatorHandler.EnviarComando(command);
    
        if (!result.IsValid)
        {
            await _userManager.DeleteAsync(user);
            return CustomResponse(result);
        }

        var createdUser = await _usuarioQueries.ObterPorEmail(model.Email);
        return CustomResponse(createdUser);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarUsuarioInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var command = new AtualizarUsuarioCommand(id, model.Nome, model.Neurodivergencia);
        var result = await _mediatorHandler.EnviarComando(command);
        
        return CustomResponse(result);
    }
}