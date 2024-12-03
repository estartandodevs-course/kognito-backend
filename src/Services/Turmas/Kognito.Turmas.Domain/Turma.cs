using EstartandoDevsCore.DomainObjects;
using Kognito.Turmas.Domain;

public class Turma : Entity, IAggregateRoot
{ 
    private readonly List<Enturmamento> _enturmamentos;

    public Usuario Professor{ get; private set; }
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

    public Turma(Guid id,Usuario professor, string nome, string descricao, string materia, Cor cor, Icones icones)
        : this()
    {
        AtribuirEntidadeId(id);
        ValidarCampos(nome, materia);
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        Cor = cor;
        Icones = icones;

        GerarHashAcesso();
        GerarLinkAcesso();
    }
    private void GerarHashAcesso()
    {
         HashAcesso = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Replace("/", "_").Replace("+", "-").Substring(0, 8);
    }

    private void GerarLinkAcesso()
    {
        LinkAcesso = $"/turma/{Id}/{HashAcesso}";
    }


    public void AdicionarEnturmamento(Enturmamento enturmamento)
    {
        if (_enturmamentos.Any(e => e.Aluno.Id == enturmamento.Aluno.Id))
            throw new ArgumentException("Aluno já está matriculado nesta turma");

        _enturmamentos.Add(enturmamento);
    }

    public void RemoverEnturmamento(Guid alunoId)
    {
        var enturmamento = _enturmamentos.FirstOrDefault(e => e.Aluno.Id == alunoId);
        if (enturmamento != null)
            _enturmamentos.Remove(enturmamento);
    }


    private void ValidarCampos(string nome, string materia)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome não pode ser vazio");
        if (string.IsNullOrWhiteSpace(materia))
            throw new ArgumentException("Matéria não pode ser vazia");
    }

    public void AtribuirProfessor(Usuario professor) => Professor = professor ?? 
        throw new ArgumentNullException(nameof(professor));

    public void AtribuirNome(string nome) => Nome = !string.IsNullOrWhiteSpace(nome) ? 
        nome : throw new ArgumentException("Nome inválido");

    public void AtribuirDescricao(string descricao) => Descricao = descricao;

    public void AtribuirMateria(string materia) => Materia = !string.IsNullOrWhiteSpace(materia) ? 
        materia : throw new ArgumentException("Matéria inválida");

    public void AtribuirLinkAcesso(string linkAcesso) => LinkAcesso = linkAcesso;

    public void AtribuirCor(Cor cor) => Cor = cor;

    public void AtribuirIcone(Icones icones) => Icones = icones;
}