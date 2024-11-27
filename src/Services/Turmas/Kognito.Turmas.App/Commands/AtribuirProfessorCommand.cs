using System;
using EstartandoDevsCore.Messages;
using EstartandoDevsCore.ValueObjects;
using Kognito.Turmas.Domain;

namespace Kognito.Turmas.App.Commands;

public class AtribuirProfessorCommand : Command
{


    public Guid Id { get; set; }
    public Usuario Professor{ get; set; }
    public Cpf Cpf { get; set; }
    public string Name { get; set; }

    public AtribuirProfessorCommand(Guid id, Usuario professor, Cpf cpf, string name)
    {
        Id = id;
        Professor = professor;
        Cpf = cpf;
        Name = name;
    }
}
