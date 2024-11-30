using System;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.ViewModels;

public class AlunoViewModel
{

    public Guid Id { get; set; }
    public Usuario Aluno { get; set; }
    public string Neurodivergencia { get; set; }
    public int Ofensiva { get; set; }

   public static AlunoViewModel Mapear(Aluno aluno)
   {
        if(aluno == null)
            return null;

        return new AlunoViewModel
        {
            Id = aluno.Id,
            Neurodivergencia = aluno.Neurodivergencia,
            Ofensiva = aluno.Ofensiva,
        };
   }
}
