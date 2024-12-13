using EstartandoDevsCore.DomainObjects;
using Kognito.Turmas.Domain.Common;

namespace Kognito.Turmas.Domain;

public class Conteudo : Entity, IAggregateRoot
{
    public string Titulo { get; private set; }
    public string ConteudoDidatico { get; private set; }
    public Turma Turma { get; private set; }
    public Guid TurmaId { get; private set; }

    protected Conteudo() { }

    public static Result<Conteudo> Criar(string titulo, string conteudoDidatico)
    {
        var conteudo = new Conteudo();

        var tituloResult = conteudo.AtribuirTitulo(titulo);
        if (!tituloResult.Success)
            return Result<Conteudo>.Fail(tituloResult.Errors);

        var conteudoDidaticoResult = conteudo.AtribuirConteudoDidatico(conteudoDidatico);
        if (!conteudoDidaticoResult.Success)
            return Result<Conteudo>.Fail(conteudoDidaticoResult.Errors);

        return Result<Conteudo>.Ok(conteudo, "Conteúdo criado com sucesso");
    }

    public Result AtribuirTitulo(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            return Result.Fail("O título do conteúdo é obrigatório");

        Titulo = titulo;
        return Result.Ok("Título atribuído com sucesso");
    }

    public Result AtribuirConteudoDidatico(string conteudoDidatico)
    {
        if (string.IsNullOrWhiteSpace(conteudoDidatico))
            return Result.Fail("O conteúdo didático não pode ser nulo");

        ConteudoDidatico = conteudoDidatico;
        return Result.Ok("Conteúdo didático atribuído com sucesso");
    }

    public Result VincularTurma(Turma turma)
    {
        if (turma == null)
            return Result.Fail("A turma não pode ser nula");

        Turma = turma;
        TurmaId = turma.Id;
        return Result.Ok("Turma vinculada com sucesso");
    }
}