using System;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.ViewModels;

public class ConteudoViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string DidacticContent { get; set; }
    public Guid ClassId { get; set; }  
    public string ClassName { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }

    public static ConteudoViewModel Mapear(Conteudo conteudo)
    {
        if (conteudo == null) return null;

        return new ConteudoViewModel
        {
            
            Id = conteudo.Id,
            Title = conteudo.Titulo,
            DidacticContent = conteudo.ConteudoDidatico,
            ClassId = conteudo.TurmaId,
            ClassName = conteudo.Turma?.Nome,
            RegistrationDate = conteudo.DataDeCadastro,
            LastModifiedDate = conteudo.DataDeAlteracao
        };
        
    }
}