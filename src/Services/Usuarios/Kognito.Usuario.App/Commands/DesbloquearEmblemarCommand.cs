using EstartandoDevsCore.Messages;

namespace Kognito.Usuario.App.Commands;
public class DesbloquearEmblemaCommand : Command
{
    public Guid EmblemaId { get; set; }
    public DateTime DataEntrega { get; set; }
    public DateTime Prazo { get; set; }

    public DesbloquearEmblemaCommand(Guid emblemaId, DateTime dataEntrega, DateTime prazo)
    {
        EmblemaId = emblemaId;
        DataEntrega = dataEntrega;
        Prazo = prazo;
    }
}