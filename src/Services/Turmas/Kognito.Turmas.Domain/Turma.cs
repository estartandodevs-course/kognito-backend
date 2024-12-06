using EstartandoDevsCore.DomainObjects;
using Kognito.Turmas.Domain;
using Kognito.Turmas.Domain.Common;

public class Turma : Entity, IAggregateRoot
{ 
    private readonly List<Enturmamento> _enturmamentos;

    public Usuario Professor { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public string LinkAcesso { get; private set; }
    public Cor Cor { get; private set; }
    public Icones Icones { get; private set; }
    public string HashAcesso { get; private set; }
    public IReadOnlyCollection<Enturmamento> Enturmamentos => _enturmamentos;
   
    protected Turma()
    {
        _enturmamentos = new List<Enturmamento>();
    }

    public static Result<Turma> Criar(Guid id, Usuario professor, string nome, string descricao, 
        string materia, Cor cor, Icones icones)
    {
        var turma = new Turma();
        
        var validationResult = turma.ValidarCampos(nome, materia);
        if (!validationResult.Success)
            return Result<Turma>.Fail(validationResult.Errors);

        if (professor == null)
            return Result<Turma>.Fail("Professor não pode ser nulo");

        turma.AtribuirEntidadeId(id);
        turma.Professor = professor;
        turma.Nome = nome;
        turma.Descricao = descricao;
        turma.Materia = materia;
        turma.Cor = cor;
        turma.Icones = icones;

        turma.GerarHashAcesso();
        turma.GerarLinkAcesso();

        return Result<Turma>.Ok(turma, "Turma criada com sucesso");
    }

    private void GerarHashAcesso()
    {
        HashAcesso = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Replace("/", "_")
            .Replace("+", "-")
            .Substring(0, 8);
    }

    private void GerarLinkAcesso()
    {
        LinkAcesso = $"/turma/{Id}/{HashAcesso}";
    }

    public Result<Enturmamento> AdicionarEnturmamento(Enturmamento enturmamento)
    {
        if (enturmamento == null)
            return Result<Enturmamento>.Fail("Enturmamento não pode ser nulo");

        if (_enturmamentos.Any(e => e.Aluno.Id == enturmamento.Aluno.Id))
            return Result<Enturmamento>.Fail("Aluno já está matriculado nesta turma");

        _enturmamentos.Add(enturmamento);
        return Result<Enturmamento>.Ok(enturmamento, "Aluno matriculado com sucesso");
    }

    public Result RemoverEnturmamento(Guid alunoId)
    {
        var enturmamento = _enturmamentos.FirstOrDefault(e => e.Aluno.Id == alunoId);
        if (enturmamento == null)
            return Result.Fail("Aluno não encontrado na turma");

        _enturmamentos.Remove(enturmamento);
        return Result.Ok("Aluno removido da turma com sucesso");
    }

    private Result ValidarCampos(string nome, string materia)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(nome))
            errors.Add("Nome não pode ser vazio");
            
        if (string.IsNullOrWhiteSpace(materia))
            errors.Add("Matéria não pode ser vazia");

        return errors.Any() ? Result.Fail(errors) : Result.Ok();
    }

    public Result AtribuirProfessor(Usuario professor)
    {
        if (professor == null)
            return Result.Fail("Professor não pode ser nulo");

        Professor = professor;
        return Result.Ok("Professor atribuído com sucesso");
    }

    public Result AtribuirNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return Result.Fail("Nome não pode ser vazio");

        Nome = nome;
        return Result.Ok("Nome atualizado com sucesso");
    }

    public Result AtribuirMateria(string materia)
    {
        if (string.IsNullOrWhiteSpace(materia))
            return Result.Fail("Matéria não pode ser vazia");

        Materia = materia;
        return Result.Ok("Matéria atualizada com sucesso");
    }

    public void AtribuirDescricao(string descricao) => Descricao = descricao;
    public void AtribuirCor(Cor cor) => Cor = cor;
    public void AtribuirIcone(Icones icones) => Icones = icones;
}