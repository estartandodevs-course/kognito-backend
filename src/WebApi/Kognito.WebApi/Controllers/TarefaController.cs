using EstartandoDevsCore.Mediator;
using EstartandoDevsWebApiCore.Controllers;
using Kognito.Tarefas.App.Commands;
using Kognito.Tarefas.App.Queries;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Mvc; 

namespace Kognito.WebApi.Controllers;

[ApiController]
[Route("api/tarefas")]
public class TarefasController : MainController
{
    private readonly ITarefaQueries _tarefaQueries;
    private readonly IMediatorHandler _mediatorHandler;

    public TarefasController(ITarefaQueries tarefaQueries, IMediatorHandler mediatorHandler)
    {
        _tarefaQueries = tarefaQueries;
        _mediatorHandler = mediatorHandler;
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

        var command = new CriarTarefaCommand(
            id: Guid.NewGuid(),
            descricao: model.Description,
            conteudo: model.Content,
            dataFinalEntrega: model.FinalDeliveryDate,
            turmaId: model.ClassId
        );

        var result = await _mediatorHandler.EnviarComando(command);
        
        if (result.IsValid)
        {

            var tarefaCriada = await _tarefaQueries.ObterPorId(command.Id);
            return CustomResponse(new
            {
                mensagem = "Tarefa criada com sucesso!",
                tarefa = tarefaCriada
            });
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
            var tarefaAtualizada = await _tarefaQueries.ObterPorId(id);
            return CustomResponse(new
            {
                mensagem = "Tarefa atualizada com sucesso!",
                tarefa = tarefaAtualizada
            });
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

        var command = new EntregarTarefaCommand(
            conteudo: model.Content,
            alunoId: model.StudentId,
            tarefaId: id
        );

        var result = await _mediatorHandler.EnviarComando(command);
        
        if (result.IsValid)
        {
            var tarefaAtualizada = await _tarefaQueries.ObterPorId(id);
            if (tarefaAtualizada == null)
            {
                AdicionarErro("Erro ao recuperar a tarefa atualizada");
                return CustomResponse();
            }

            var entregaCriada = tarefaAtualizada.Deliveries?.LastOrDefault();
            if (entregaCriada == null)
            {
                AdicionarErro("Erro ao recuperar a entrega criada");
                return CustomResponse();
            }

            return CustomResponse(new
            {
                message = "Entrega realizada com sucesso!",
                delivery = new
                {
                    id = entregaCriada.Id,
                    content = entregaCriada.Content,
                    deliveredOn = entregaCriada.DeliveredOn,
                    studentId = entregaCriada.StudentId,
                    taskId = entregaCriada.TaskId,
                    isLate = entregaCriada.IsLate
                }
            });
        }

        return CustomResponse(result);
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
[HttpPost("entregas/{entregaId:guid}/notas")]
public async Task<IActionResult> AtribuirNota(Guid entregaId, [FromBody] GradeInputModel model)
{
    if (!ModelState.IsValid) return CustomResponse(ModelState);

    // Primeiro, busca a entrega
    var entrega = await _tarefaQueries.ObterEntregaPorId(entregaId);
    if (entrega == null)
    {
        AdicionarErro("Entrega não encontrada!");
        return CustomResponse();
    }

    // Verifica se a entrega pertence ao aluno
    if (entrega.StudentId != model.StudentId)
    {
        AdicionarErro("Entrega não encontrada para este aluno!");
        return CustomResponse();
    }

    var command = new AtribuirNotaCommand(
        valorNota: model.GradeValue,
        alunoId: model.StudentId,
        turmaId: model.ClassId,
        entregaId: entregaId,
        tarefaId: entrega.TaskId
    );

    var result = await _mediatorHandler.EnviarComando(command);
    
    if (result.IsValid)
    {
        // Busca a entrega atualizada com a nota
        var entregaAtualizada = await _tarefaQueries.ObterEntregaPorId(entregaId);
        
        return CustomResponse(new
        {
            message = "Nota atribuída com sucesso!",
            delivery = new
            {
                id = entregaAtualizada.Id,
                content = entregaAtualizada.Content,
                deliveredOn = entregaAtualizada.DeliveredOn,
                studentId = entregaAtualizada.StudentId,
                taskId = entregaAtualizada.TaskId,
                isLate = entregaAtualizada.IsLate,
                grades = entregaAtualizada.Grades?.Select(n => new
                {
                    value = n.GradeValue,
                    studentId = n.StudentId,
                    classId = n.ClassId
                })
            }
        });
    }

    return CustomResponse(result);
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
        if (tarefa == null)
        {
            AdicionarErro("Tarefa não encontrada");
            return CustomResponse();
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
    /// Obtém todas as tarefas com notas de uma turma específica
    /// </summary>
    /// <param name="turmaId">ID da turma</param>
    /// <returns>Lista de tarefas com suas respectivas notas</returns>
    /// <response code="200">Retorna a lista de tarefas com notas da turma</response>
    /// <response code="400">Quando o ID da turma é inválido</response>
    [HttpGet("turma/{turmaId:guid}/notas")]
    public async Task<IActionResult> ObterTarefasComNotasPorTurma(Guid turmaId)
    {
        if (turmaId == Guid.Empty)
        {
            AdicionarErro("Id da turma inválido");
            return BadRequest();
        }
        var tarefas = await _tarefaQueries.ObterTarefasComNotasPorTurma(turmaId);
        return CustomResponse(tarefas);
    }
}