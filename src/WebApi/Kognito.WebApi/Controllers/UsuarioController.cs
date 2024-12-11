using System.Security.Claims;
using EstartandoDevsWebApiCore.Controllers;
using EstartandoDevsCore.Mediator;
using EstartandoDevsCore.ValueObjects;
using Kognito.Tarefas.App.Queries;
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
    private readonly ITarefaQueries _tarefaQueries;
    private readonly UserManager<IdentityUser> _userManager;

    public UsuariosController(
        IUsuarioQueries usuarioQueries,
        ITarefaQueries tarefaQueries,
        IMediatorHandler mediatorHandler,
        UserManager<IdentityUser> userManager)
    {
        _usuarioQueries = usuarioQueries;
        _mediatorHandler = mediatorHandler;
        _userManager = userManager;
        _tarefaQueries = tarefaQueries;

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
    /// Obtém o perfil do usuário autenticado
    /// </summary>
    /// <returns>Dados do perfil do usuário</returns>
    /// <response code="200">Retorna os dados do usuário</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [HttpGet]
    [Authorize]
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
    
    /// <summary>
    /// Obtém os dados de um usuário específico por ID
    /// </summary>
    /// <param name="id">ID do usuário</param>
    /// <returns>Dados do usuário solicitado</returns>
    /// <response code="200">Retorna os dados do usuário</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [Authorize]
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

    /// <summary>
    /// Obtém os emblemas conquistados pelo usuário
    /// </summary>
    /// <returns>Lista de emblemas do usuário</returns>
    /// <response code="200">Retorna a lista de emblemas</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [Authorize]
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

    /// <summary>
    /// Obtém a ofensiva atual do usuário
    /// </summary>
    /// <returns>Dados da ofensiva do usuário</returns>
    /// <response code="200">Retorna os dados da ofensiva</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [Authorize]
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

    /// <summary>
    /// Altera a senha do usuário autenticado
    /// </summary>
    /// <param name="model">Dados contendo senha atual e nova senha</param>
    /// <returns>Confirmação da alteração de senha</returns>
    /// <response code="200">Senha alterada com sucesso</response>
    /// <response code="400">Quando os dados são inválidos</response>
    /// <response code="404">Quando o usuário não é encontrado</response>

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

    /// <summary>
    /// Cria um novo usuário professor
    /// </summary>
    /// <param name="model">Dados do professor</param>
    /// <returns>Dados do professor criado</returns>
    /// <response code="200">Professor criado com sucesso</response>
    /// <response code="400">Quando os dados são inválidos</response>
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

        var identidadeCriada = await _userManager.CreateAsync(user, model.Password);

        if (!identidadeCriada.Succeeded)
        {
            foreach (var erro in identidadeCriada.Errors)
                AdicionarErro(erro.Description);

            return CustomResponse();
        }

        var command = new CriarProfessorCommand(
            model.Name,
            model.Cpf,
            model.Email,
            model.Password
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

    /// <summary>
    /// Cria um novo usuário aluno
    /// </summary>
    /// <param name="model">Dados do aluno</param>
    /// <returns>Dados do aluno criado</returns>
    /// <response code="200">Aluno criado com sucesso</response>
    /// <response code="400">Quando os dados são inválidos</response>
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
            model.parentEmail
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
    
    /// <summary>
    /// Adiciona uma neurodivergência ao perfil do usuário
    /// </summary>
    /// <param name="model">Dados da neurodivergência</param>
    /// <returns>Confirmação da adição</returns>
    /// <response code="200">Neurodivergência adicionada com sucesso</response>
    /// <response code="400">Quando os dados são inválidos</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
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

        var command = new AdicionarNeurodivergenciaCommand(usuarioId.Value, model.parentCode, model.Neurodivergence.ToString());
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse(result);
    }
    
    /// <summary>
    /// Atualiza o perfil do usuário autenticado
    /// </summary>
    /// <param name="model">Dados do perfil a serem atualizados</param>
    /// <returns>Dados do perfil atualizado</returns>
    /// <response code="200">Perfil atualizado com sucesso</response>
    /// <response code="400">Quando os dados são inválidos</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [Authorize]
    [HttpPut("perfil")]
    public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarPerfilInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return NotFound();
        }

        var command = new AtualizarUsuarioCommand(usuarioId.Value, model.Name, model.Email);
        var result = await _mediatorHandler.EnviarComando(command);

        if (!result.IsValid) return CustomResponse(result);

        var usuarioAtualizado = await _usuarioQueries.ObterPorId(usuarioId.Value);
        return CustomResponse(usuarioAtualizado);
    }
    
    /// <summary>
    /// Solicita recuperação de senha
    /// </summary>
    /// <param name="model">Email do usuário</param>
    /// <returns>Resultado da solicitação</returns>
    /// <response code="200">Email de recuperação enviado com sucesso</response>
    /// <response code="400">Quando os dados são inválidos</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [HttpPost("esqueceu-senha")]
    public async Task<IActionResult> EsqueceuSenha([FromBody] EsqueceuSenhaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var command = new EsqueceuSenhaCommand(model.Email);
        var result = await _mediatorHandler.EnviarComando(command);

        if (!result.IsValid) return CustomResponse(result);

        return CustomResponse("Email de recuperação enviado com sucesso!");
    }
    
    /// <summary>
    /// Redefine a senha do usuário usando o código de recuperação
    /// </summary>
    /// <param name="model">Dados para redefinição de senha</param>
    /// <returns>Resultado da operação</returns>
    /// <response code="200">Senha redefinida com sucesso</response>
    /// <response code="400">Quando os dados são inválidos</response>
    /// <response code="404">Quando o usuário não é encontrado</response>
    [HttpPost("redefinir-senha")]
    public async Task<IActionResult> RedefinirSenha([FromBody] RedefinirSenhaInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var command = new RedefinirSenhaCommand(model.Email, model.RecoveryCode, model.NewPassword);
        var result = await _mediatorHandler.EnviarComando(command);

        return CustomResponse(result);
    }
    
    /// <summary>
    /// Obtém o gráfico de desempenho do aluno em todas as suas turmas
    /// </summary>
    /// <returns>Dados de desempenho do aluno</returns>
    /// <response code="200">Retorna os dados de desempenho</response>
    /// <response code="401">Quando o usuário não está autenticado</response>
    [HttpGet("desempenho/aluno")]
    public async Task<IActionResult> ObterDesempenhoAluno()
    {
        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var isAluno = await _usuarioQueries.VerificarTipoUsuario(usuarioId.Value);
        if (!isAluno)
        {
            return BadRequest(new
            {
                success = false,
                message = "Apenas alunos podem acessar esta rota"
            });
        }

        var desempenho = await _tarefaQueries.ObterDesempenhoAluno(usuarioId.Value);
        return CustomResponse(desempenho);
    }

}