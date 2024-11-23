using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EstartandoDevsCore.DomainObjects;

namespace Kognito.Turmas.Domain;

public class Aluno{
    public Guid Id { get; set; }
    public string Neurodivergencia { get; set; }
    public int Ofensiva { get; private set; }
        
    public Aluno(Guid id, string neurodivergencia, int ofensiva)
    {
        Id = id;
        Neurodivergencia = neurodivergencia;
        Ofensiva = ofensiva;
    }
    
}
