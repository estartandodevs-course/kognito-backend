using EstartandoDevsCore.Data;
using Kognito.Tarefas.Domain.interfaces;
using Kognito.Usuarios.App.Domain.Interface;

namespace Kognito.Usuarios.App.Domain.Services;

public class EmblemaService
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IEmblemaRepository _emblemaRepository;

    public EmblemaService(ITarefaRepository tarefaRepository, IEmblemaRepository emblemaRepository)
    {
        _tarefaRepository = tarefaRepository;
        _emblemaRepository = emblemaRepository;
    }

    public async Task VerificarEDesbloquearEmblemasAsync(Guid alunoId)
    {
        // Obter todas as tarefas entregues pelo aluno
        var tarefas = await _tarefaRepository.ObterPorAlunoAsync(alunoId);
        var tarefasEntregues = tarefas.Where(t => t.Entregue).ToList();

        // Contar as entregas feitas
        int totalEntregas = tarefasEntregues.Count();

        // Calcular quantos emblemas o aluno deveria ter
        int emblemasParaDesbloquear = totalEntregas / 3;

        // Obter emblemas já desbloqueados pelo aluno
        var emblemasDesbloqueados = await _emblemaRepository.ObterPorAlunoAsync(alunoId);

        // Desbloquear novos emblemas, se necessário
        for (int i = emblemasDesbloqueados.Count(); i < emblemasParaDesbloquear; i++)
        {
            var emblema = new Emblemas($"Emblema {i + 1}", $"Desbloqueado após {3 * (i + 1)} entregas");
            emblema.Desbloquear();

            // Persistir o novo emblema
            await _emblemaRepository.AdicionarAsync(emblema);
        }
    }
}

