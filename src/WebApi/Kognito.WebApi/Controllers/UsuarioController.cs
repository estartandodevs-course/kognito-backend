using System.Security.Claims;
using EstartandoDevsWebApiCore.Controllers;
using EstartandoDevsCore.Mediator;
using EstartandoDevsCore.ValueObjects;
using Kognito.Usuarios.App.Commands;
using Kognito.Usuarios.App.Queries;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

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


    [HttpGet]
    public async Task<IActionResult> ObterPerfil()
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var usuario = await _usuarioQueries.ObterPorId(usuarioId.Value);

        return CustomResponse(usuario);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuario = await _usuarioQueries.ObterPorId(id);

        if (usuario == null)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        return CustomResponse(usuario);
    }

    [HttpGet("/emblemas")]
    public async Task<IActionResult> ObterEmblemas()
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var emblemas = await _usuarioQueries.ObterEmblemas(usuarioId.Value);
        return CustomResponse(emblemas);
    }

    [HttpGet("/ofensiva")]
    public async Task<IActionResult> ObterOfensiva()
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var ofensiva = await _usuarioQueries.ObterOfensiva(usuarioId.Value);

        return CustomResponse(ofensiva);
    }

    // [HttpPost]
    // public async Task<IActionResult> CriarUsuario([FromBody] UsuarioInputModel model)
    // {
    //     if (!ModelState.IsValid) return CustomResponse(ModelState);
    //
    //     var user = new IdentityUser()
    //     {
    //         Id = Guid.NewGuid().ToString(),
    //         UserName = model.Email,
    //         NormalizedUserName = model.Email.ToUpper(),
    //         Email = model.Email,
    //         NormalizedEmail = model.Email.ToUpper(),
    //         EmailConfirmed = true
    //     };
    //
    //     var identidadeCriada = await _userManager.CreateAsync(user, model.Senha);
    //
    //     if (!identidadeCriada.Succeeded)
    //     {
    //         foreach (var erro in identidadeCriada.Errors)
    //             AdicionarErro(erro.Description);
    //
    //         return CustomResponse();
    //     }
    //
    //     var command = new CriarUsuarioCommand(
    //         model.Nome,
    //         model.Cpf,
    //         model.Neurodivergencia,
    //         model.Email,
    //         model.Senha
    //     );
    //
    //     var result = await _mediatorHandler.EnviarComando(command);
    //
    //     if (!result.IsValid)
    //     {
    //         await _userManager.DeleteAsync(user);
    //         return CustomResponse(result);
    //     }
    //
    //     var createdUser = await _usuarioQueries.ObterPorEmail(model.Email);
    //     return CustomResponse(createdUser);
    // }

    // [HttpPut]
    // public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioInputModel model)
    // {
    //     if (!ModelState.IsValid) return CustomResponse(ModelState);
    //
    //     var usuarioId = ObterUsuarioId();
    //     if (!usuarioId.HasValue)
    //     {
    //         AdicionarErro("Usuário não encontrado");
    //         return NotFound();
    //     }
    //
    //     var command = new AtualizarUsuarioCommand(usuarioId.Value, model.Nome, model.Neurodivergencia);
    //     var result = await _mediatorHandler.EnviarComando(command);
    //
    //     return CustomResponse(result);
    // }


    [Authorize]
    [HttpPost("mudar-senha")]
    public async Task<ActionResult> ChangePassword(AlterarSenhaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var identityId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(identityId))
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var user = await _userManager.FindByIdAsync(identityId);
        if (user == null)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                AdicionarErro(error.Description);

            return CustomResponse();
        }

        return CustomResponse("Senha alterada com sucesso");
    }

    [HttpPost("professores")]
    public async Task<IActionResult> CriarProfessor([FromBody] ProfessorInputModel model)
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

        var command = new CriarProfessorCommand(
            model.Nome,
            model.Cpf,
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

    [HttpPost("alunos")]
    public async Task<IActionResult> CriarAluno([FromBody] AlunoInputModel model)
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

        var identidadeCriada = await _userManager.CreateAsync(user, model.Password);

        if (!identidadeCriada.Succeeded)
        {
            foreach (var erro in identidadeCriada.Errors)
                AdicionarErro(erro.Description);

            return CustomResponse();
        }

        var command = new CriarAlunoCommand(
            model.Name,
            model.Cpf,
            model.Email,
            model.Password,
            model.EmailResponsavel
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

    [Authorize]
    [HttpPost("adicionar-neurodivergencia")]
    public async Task<IActionResult> AdicionarNeurodivergencia([FromBody] AdicionarNeurodivergenciaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var command = new AdicionarNeurodivergenciaCommand(usuarioId.Value, model.CodigoPai, model.Neurodivergencia);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse(result);
    }
}