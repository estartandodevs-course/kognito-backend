using EstartandoDevsCore.DomainObjects;
using Kognito.Turmas.Domain;

public class Turma : Entity, IAggregateRoot
{ 
    private readonly List<Enturmamento> _enturmamentos;
    private readonly List<LinkDeAcesso> _linksDeAcesso;

    public Usuario Professor{ get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public string Materia { get; private set; }
    public Cor Cor { get; private set; }
    public Icones Icones { get; private set; }
    
    public IReadOnlyCollection<Enturmamento> Enturmamentos => _enturmamentos;
    public IReadOnlyCollection<LinkDeAcesso> LinksDeAcesso => _linksDeAcesso;

    protected Turma()
    {
        _enturmamentos = new List<Enturmamento>();
        _linksDeAcesso = new List<LinkDeAcesso>();
    }

    public Turma(Usuario professor, string nome, string descricao, string materia, Cor cor, Icones icones)
        : this()
    {
        ValidarCampos(nome, materia);
        Professor = professor;
        Nome = nome;
        Descricao = descricao;
        Materia = materia;
        Cor = cor;
        Icones = icones;
    }

   
    public void AdicionarEnturmamento(Enturmamento enturmamento)
    {
        if (_enturmamentos.Any(e => e.Aluno.Id == enturmamento.Aluno.Id))
            throw new DomainException("Aluno já está matriculado nesta turma");

        _enturmamentos.Add(enturmamento);
    }
    public async Task MatricularAluno(Usuario aluno, string codigo)
    {
        if (aluno == null)
            throw new DomainException("Aluno inválido");

        if (_enturmamentos.Any(e => e.Aluno.Id == aluno.Id))
            throw new DomainException("Aluno já está matriculado nesta turma");

        var link = _linksDeAcesso.FirstOrDefault(l => l.Codigo == codigo);
        
        if (link == null)
            throw new DomainException("Link de acesso inválido");

        lock(link)
        {
            if (!link.PodeSerUtilizado())
                throw new DomainException("Link de acesso expirado ou atingiu limite de usos");
                
            link.Utilizar();
        }
        var enturmamento = new Enturmamento(aluno, this, Enturmamento.EnturtamentoStatus.Ativo);
        _enturmamentos.Add(enturmamento);
    }

    public void RemoverEnturmamento(Guid alunoId)
    {
        var enturmamento = _enturmamentos.FirstOrDefault(e => e.Aluno.Id == alunoId);
        if (enturmamento != null)
            _enturmamentos.Remove(enturmamento);
    }

    public bool AlunoEstaMatriculado(Guid alunoId)
    {
        return _enturmamentos.Any(e => e.Aluno.Id == alunoId);
    }

    
    public LinkDeAcesso GerarLinkDeAcesso(int? limiteUsos = null, int? diasValidade = null)
    {
        lock(_linksDeAcesso)
        {
            var link = new LinkDeAcesso(
                limiteUsos ?? 0,
                diasValidade ?? 1
            );
            _linksDeAcesso.Add(link);
            return link;
        }
    }

    public async Task IngressarPorLink(Usuario aluno, string codigo)
    {
        await MatricularAluno(aluno, codigo);
    }

    public void DesativarLink(string codigo)
    {
        var link = ObterLinkPorCodigo(codigo);
        link.Desativar();
    }

    public void RemoverLink(string codigo)
    {
        var link = ObterLinkPorCodigo(codigo);
        _linksDeAcesso.Remove(link);
    }

    public LinkDeAcesso ObterLinkPorCodigo(string codigo)
    {
        var link = _linksDeAcesso.FirstOrDefault(l => l.Codigo == codigo);
        if (link == null)
            throw new DomainException("Link de acesso não encontrado");

        return link;
    }

    public bool ValidarLink(string codigo)
    {
        var link = _linksDeAcesso.FirstOrDefault(l => l.Codigo == codigo);
        return link?.PodeSerUtilizado() ?? false;
    }

    public void LimparLinksExpirados()
    {
        var linksExpirados = _linksDeAcesso.Where(l => !l.PodeSerUtilizado()).ToList();
        foreach (var link in linksExpirados)
        {
            _linksDeAcesso.Remove(link);
        }
    }

    public void AtribuirProfessor(Usuario professor)
    {
        if (professor == null)
            throw new DomainException("Professor inválido");

        Professor = professor;
    }

    public void AtribuirNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome inválido");

        Nome = nome;
    }

    public void AtribuirDescricao(string descricao)
    {
        Descricao = descricao;
    }

    public void AtribuirMateria(string materia)
    {
        if (string.IsNullOrWhiteSpace(materia))
            throw new DomainException("Matéria inválida");

        Materia = materia;
    }

    public void AtribuirCor(Cor cor)
    {
        Cor = cor;
    }

    public void AtribuirIcone(Icones icones)
    {
        Icones = icones;
    }


    public int ObterQuantidadeAlunos()
    {
        return _enturmamentos.Count;
    }

    public int ObterQuantidadeLinksAtivos()
    {
        return _linksDeAcesso.Count(l => l.PodeSerUtilizado());
    }

    public IEnumerable<Usuario> ObterAlunosMatriculados()
    {
        return _enturmamentos.Select(e => e.Aluno);
    }


    private void ValidarCampos(string nome, string materia)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new DomainException("Nome não pode ser vazio");
        if (string.IsNullOrWhiteSpace(materia))
            throw new DomainException("Matéria não pode ser vazia");
    }


    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;

        var other = (Turma)obj;
        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}