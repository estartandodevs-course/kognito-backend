using EstartandoDevsCore.Messages;

namespace Kognito.Usuario.App.Commands;

public class CriarEmblemaCommand : Command
{
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public CriarEmblemaCommand(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
    }
}

