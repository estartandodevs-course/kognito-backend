using EstartandoDevsCore.DomainObjects;

namespace Kognito.Turmas.Domain;

public class Aluno
{
    public Guid Id { get; private set; }
    public string Neurodivergencia { get; private set; }
    public int Ofensiva { get; private set; }
        
    public Aluno(Guid id, string neurodivergencia, int ofensiva)
    {
        Id = id;
        Neurodivergencia = neurodivergencia;
        Ofensiva = ofensiva;
    }
    private Aluno(){}
    
    public void AtribuirNeurodivergencia(string neurodivergencia) => Neurodivergencia = neurodivergencia;

    public void AcrescentaDiaOfensiva()
    {
        Ofensiva++;
    }

    public void ResetaOfensiva()
    {
        Ofensiva = 0;
    }
}
