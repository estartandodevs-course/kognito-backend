using EstartandoDevsCore.Mediator;
using EstartandoDevsWebApiCore.Controllers;
using Kognito.Tarefas.App.Commands;
using Kognito.Tarefas.App.Queries;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Mvc;
using Kognito.Turmas.App;
using Kognito.Turmas.App.Queries;
using Kognito.Usuarios.App.Domain;
using Kognito.Usuarios.App.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Kognito.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/tarefas")]
public class TarefasController : MainController
{
    private readonly ITarefaQueries _tarefaQueries;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly ITurmaQueries _turmaQueries;
    private readonly IUsuarioQueries _usuarioQueries;


    public TarefasController(ITarefaQueries tarefaQueries, IMediatorHandler mediatorHandler,
        IUsuarioQueries usuarioQueries,
        ITurmaQueries turmaQueries)
    {
        _tarefaQueries = tarefaQueries;
        _mediatorHandler = mediatorHandler;
        _turmaQueries = turmaQueries;
        _usuarioQueries = usuarioQueries;
    }
    
    private Guid? ObterUsuarioId()
    {
        var userIdClaim = User.FindFirst("userId")?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            return null;

        return userId;
    }

    /// <summary>
    /// Cria uma nova tarefa
    /// </summary>
    /// <param name="model">Dados da tarefa contendo descrição, conteúdo, data final de entrega e ID da turma</param>
    /// <returns>Dados da tarefa criada</returns>
    /// <response code="200">Retorna a tarefa recém-criada</response>
    /// <response code="400">Quando os dados da requisição são inválidos</response>
    /// <response code="500">Quando ocorre um erro interno ao criar a tarefa</response>
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] TaskInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);
        
        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }
        
        var isProfessor = await _turmaQueries.VerificarProfessorTurma(model.ClassId, usuarioId.Value);
        if (!isProfessor)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não tem permissão para criar uma tarefa nesta turma."
            });
        }

        var command = new CriarTarefaCommand(
            Guid.NewGuid(),
            model.Description,
            model.Content,
            model.FinalDeliveryDate,
            model.ClassId,
            model.NeurodivergenceTargets
        );

        var result = await _mediatorHandler.EnviarComando(command);

        if (result.IsValid)
        {
            return CustomResponse("Tarefa criada com sucesso!");
        }

        return CustomResponse(result);
    }

    /// <summary>
    /// Obtém uma tarefa específica por ID
    /// </summary>
    /// <param name="id">ID da tarefa</param>
    /// <returns>Dados detalhados da tarefa</returns>
    /// <response code="200">Retorna os dados da tarefa solicitada</response>
    /// <response code="404">Quando a tarefa não é encontrada</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var tarefa = await _tarefaQueries.ObterPorId(id);
        if (tarefa == null)
        {
            AdicionarErro("Tarefa não econtrada.");
            return CustomResponse();
        }

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var isProfessor = await _turmaQueries.VerificarProfessorTurma(tarefa.ClassId, usuarioId.Value);
        var isEnrolledStudent = await _turmaQueries.VerificarAlunoTurma(tarefa.ClassId, usuarioId.Value);

        if (!isProfessor && !isEnrolledStudent)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não tem permissão para acessar esta tarefa"
            });
        }

        return CustomResponse(tarefa);
    }

    /// <summary>
    /// Atualiza uma tarefa existente
    /// </summary>
    /// <param name="id">ID da tarefa a ser atualizada</param>
    /// <param name="model">Novos dados da tarefa</param>
    /// <returns>Dados atualizados da tarefa</returns>
    /// <response code="200">Retorna a tarefa com dados atualizados</response>
    /// <response code="400">Quando os dados da requisição são inválidos</response>
    /// <response code="404">Quando a tarefa não é encontrada</response>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] TaskInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var command = new AtualizarTarefaCommand(
            id: id,
            descricao: model.Description,
            conteudo: model.Content,
            dataFinalEntrega: model.FinalDeliveryDate,
            turmaId: model.ClassId
        );

        var result = await _mediatorHandler.EnviarComando(command);

        if (result.IsValid)
        {
            return CustomResponse("Tarefa atualizada com sucesso!");
        }

        return CustomResponse(result);
    }


    /// <summary>
    /// Registra a entrega de uma tarefa por um aluno
    /// </summary>
    /// <param name="id">ID da tarefa</param>
    /// <param name="model">Dados da entrega contendo conteúdo e ID do aluno</param>
    /// <returns>Dados da entrega registrada</returns>
    /// <response code="200">Retorna os dados da entrega realizada</response>
    /// <response code="400">Quando os dados da requisição são inválidos</response>
    /// <response code="404">Quando a tarefa não é encontrada</response>
    [HttpPost("{id:guid}/entregas")]
public async Task<IActionResult> EntregarTarefa(Guid id, [FromBody] DeliveryInputModel model)
{
    if (!ModelState.IsValid) return CustomResponse(ModelState);

    var usuarioId = ObterUsuarioId();
    if (!usuarioId.HasValue)
    {
        AdicionarErro("Usuário não encontrado");
        return CustomResponse();
    }

    var tarefa = await _tarefaQueries.ObterPorId(id);
    if (tarefa == null)
    {
        AdicionarErro("Tarefa não encontrada");
        return CustomResponse();
    }

    var isEnrolledStudent = await _turmaQueries.VerificarAlunoTurma(tarefa.ClassId, usuarioId.Value);
    if (!isEnrolledStudent)
    {
        return BadRequest(new
        {
            success = false,
            message = "Você não está matriculado nesta turma"
        });
    }
    

    var entregas = await _tarefaQueries.ObterEntregasPorTarefa(id);
    if (entregas.Any(e => e.StudentId == usuarioId.Value))
    {
        return BadRequest(new
        {
            success = false,
            message = "Você já entregou esta tarefa"
        });
    }

    var command = new EntregarTarefaCommand(
        conteudo: model.Content,
        alunoId: usuarioId.Value,
        tarefaId: id
    );

    var result = await _mediatorHandler.EnviarComando(command);
    return result.IsValid ? 
        CustomResponse("Entrega realizada com sucesso!") : 
        CustomResponse(result);
}

    /// <summary>
    /// Atribui uma nota a uma entrega específica
    /// </summary>
    /// <param name="entregaId">ID da entrega</param>
    /// <param name="model">Dados da nota contendo valor, ID do aluno e ID da turma</param>
    /// <returns>Dados da entrega com a nota atribuída</returns>
    /// <response code="200">Retorna os dados da entrega com a nota</response>
    /// <response code="400">Quando os dados da requisição são inválidos ou a entrega não é encontrada</response>
    /// <response code="404">Quando a entrega não é encontrada</response>
    [HttpPost("entregas/{entregaId:guid}")]
    public async Task<IActionResult> AtribuirNota(Guid entregaId, [FromBody] GradeInputModel model)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var entrega = await _tarefaQueries.ObterEntregaPorId(entregaId);
        if (entrega == null)
        {
            AdicionarErro("Entrega não encontrada!");
            return CustomResponse();
        }

        var tarefa = await _tarefaQueries.ObterPorId(entrega.TaskId);
        if (tarefa == null)
        {
            AdicionarErro("Tarefa não encontrada");
            return CustomResponse();
        }

        var isProfessor = await _turmaQueries.VerificarProfessorTurma(tarefa.ClassId, usuarioId.Value);
        if (!isProfessor)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não tem permissão para atribuir notas"
            });
        }

        var command = new AtribuirNotaCommand(
            valorNota: model.GradeValue,
            alunoId: entrega.StudentId,
            turmaId: tarefa.ClassId,
            entregaId: entregaId,
            tarefaId: entrega.TaskId
        );

        var result = await _mediatorHandler.EnviarComando(command);
        return result.IsValid ? 
            CustomResponse("Nota atribuída com sucesso!") : 
            CustomResponse(result);
    }

    /// <summary>
    /// Remove uma tarefa específica
    /// </summary>
    /// <param name="id">ID da tarefa a ser removida</param>
    /// <returns>Confirmação da remoção</returns>
    /// <response code="200">Retorna confirmação da remoção da tarefa</response>
    /// <response code="404">Quando a tarefa não é encontrada</response>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        var tarefa = await _tarefaQueries.ObterPorId(id);
        var usuarioId = ObterUsuarioId();

        if (tarefa == null)
        {
            AdicionarErro("Tarefa não encontrada");
            return CustomResponse();
        }
        
        var isProfessor = await _turmaQueries.VerificarProfessorTurma(tarefa.ClassId, usuarioId.Value);
        if (!isProfessor)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não tem permissão para remover uma tarefa."
            });
        }

        var command = new RemoverTarefaCommand(id);
        var result = await _mediatorHandler.EnviarComando(command);

        if (result.IsValid)
        {
            return CustomResponse(new
            {
                mensagem = "Tarefa removida com sucesso!",
                tarefaRemovida = tarefa
            });
        }

        return CustomResponse(result);
    }


    /// <summary>
    /// Obtém todas as tarefas de uma turma específica
    /// </summary>
    /// <param name="turmaId">ID da turma</param>
    /// <returns>Lista de tarefas com suas notas</returns>
    /// <response code="200">Retorna a lista de tarefas </response>
    /// <response code="400">Quando o ID da turma é inválido</response>

    [HttpGet("turma/{turmaId:guid}")]
    public async Task<IActionResult> ObterTarefasPorTurma(Guid turmaId)
    {
        if (turmaId == Guid.Empty)
        {
            AdicionarErro("Id da turma inválido");
            return CustomResponse();
        }

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var isProfessor = await _turmaQueries.VerificarProfessorTurma(turmaId, usuarioId.Value);
        var isEnrolledStudent = await _turmaQueries.VerificarAlunoTurma(turmaId, usuarioId.Value);

        if (!isProfessor && !isEnrolledStudent)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não tem permissão para acessar as tarefas desta turma"
            });
        }

        var tarefas = await _tarefaQueries.ObterTarefasPorTurma(turmaId);
        return CustomResponse(tarefas);
    }
    
    /// <summary>
    /// Obtém todas as tarefas de uma turma específica na pespectiva do professor
    /// </summary>
    /// <param name="turmaId">ID da turma</param>
    /// <returns>Lista de tarefas</returns>
    /// <response code="200">Retorna a lista de tarefas </response>
    /// <response code="400">Quando o ID da turma é inválido</response>

    [HttpGet("professores/turmas/{turmaId:guid}")]
    public async Task<IActionResult> ObterTarefasPorTurmaProfessor(Guid turmaId)
    {
        if (turmaId == Guid.Empty)
        {
            AdicionarErro("Id da turma inválido");
            return CustomResponse();
        }

        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var isProfessor = await _turmaQueries.VerificarProfessorTurma(turmaId, usuarioId.Value);

        if (!isProfessor)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não tem permissão para acessar as tarefas desta turma"
            });
        }

        var tarefas = await _tarefaQueries.ObterTarefasPorTurma(turmaId);
        return CustomResponse(tarefas);
    }
    
    /// <summary>
    /// Obtém todas as notas de uma tarefa específica
    /// </summary>
    /// <param name="id">ID da tarefa</param>
    /// <returns>Lista de notas da tarefa</returns>
    /// <response code="200">Retorna a lista de notas da tarefa</response>
    /// <response code="404">Quando a tarefa não é encontrada</response>
    [HttpGet("professores/notas/{id:guid}")]
    public async Task<IActionResult> ObterNotasPorTarefa(Guid id)
    {
        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var tarefa = await _tarefaQueries.ObterPorId(id);
        if (tarefa == null)
        {
            AdicionarErro("Tarefa não encontrada");
            return CustomResponse();
        }
        
        var isProfessor = await _turmaQueries.VerificarProfessorTurma(tarefa.ClassId, usuarioId.Value);
        if (!isProfessor)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não tem permissão para visualizar as notas desta tarefa"
            });
        }

        var notas = await _tarefaQueries.ObterNotasPorTarefa(id);
        
        return CustomResponse(notas);
    }
    
    /// <summary>
    /// Obtém todas as entregas de uma tarefa específica
    /// </summary>
    /// <param name="id">ID da tarefa</param>
    /// <returns>Lista de entregas da tarefa</returns>
    /// <response code="200">Retorna a lista de entregas da tarefa</response>
    /// <response code="404">Quando a tarefa não é encontrada</response>
    [HttpGet("professores/entregas/{id:guid}")]
    public async Task<IActionResult> ObterEntregasPorTarefa(Guid id)
    {
        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var tarefa = await _tarefaQueries.ObterPorId(id);
        if (tarefa == null)
        {
            AdicionarErro("Tarefa não encontrada");
            return CustomResponse();
        }

        var isProfessor = await _turmaQueries.VerificarProfessorTurma(tarefa.ClassId, usuarioId.Value);
        if (!isProfessor)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não tem permissão para visualizar as entregas desta tarefa"
            });
        }

        var entregas = await _tarefaQueries.ObterEntregasPorTarefa(id);
        return CustomResponse(entregas);
    }
    
    [HttpGet("aluno/turma/{turmaId:guid}")]
    public async Task<IActionResult> ObterTarefasParaAluno(Guid turmaId)
    {
        var usuarioId = ObterUsuarioId();
        if (!usuarioId.HasValue)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var isAluno = await _turmaQueries.VerificarAlunoTurma(turmaId, usuarioId.Value);
        if (!isAluno)
        {
            return BadRequest(new
            {
                success = false,
                message = "Você não está matriculado nesta turma"
            });
        }

        var usuario = await _usuarioQueries.ObterPorId(usuarioId.Value);
        if (usuario == null)
        {
            AdicionarErro("Usuário não encontrado");
            return CustomResponse();
        }

        var tarefas = await _tarefaQueries.ObterTarefasFiltradas(turmaId, usuario.Neurodivergencia);
        return CustomResponse(tarefas);
    }
}