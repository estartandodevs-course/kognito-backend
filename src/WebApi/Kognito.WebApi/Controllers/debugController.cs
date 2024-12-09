using Kognito.Usuarios.App.Queries;

namespace Kognito.WebApi.Controllers;

using EstartandoDevsWebApiCore.Controllers;
using Kognito.Usuarios.App.Queries;
using Microsoft.AspNetCore.Mvc;

[Route("api/debug")]
public class DebugController : MainController
{
    private readonly IUsuarioQueries _usuarioQueries;

    public DebugController(IUsuarioQueries usuarioQueries)
    {
        _usuarioQueries = usuarioQueries;
    }

    [HttpGet("codes/{email}")]
    public async Task<IActionResult> ObterCodigos(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            AdicionarErro("Email inválido");
            return CustomResponse();
        }

        var codigoRecuperacao = await _usuarioQueries.ObterCodigoRecuperacaoPorEmail(email);
        var codigoPai = await _usuarioQueries.ObterCodigoPaiPorEmail(email);

        return CustomResponse(new
        {
            email,
            codigoRecuperacao,
            codigoPai
        });
    }
}