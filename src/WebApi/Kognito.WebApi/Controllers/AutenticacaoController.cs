
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EstartandoDevsWebApiCore.Controllers;
using EstartandoDevsWebApiCore.Identidade;
using Kognito.Usuarios.App.Domain.Interface;
using Kognito.WebApi.InputModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Kognito.WebApi.Controllers;

[Route("api/identidade")]
public class AutenticacaoController : MainController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IUsuariosRepository _usuariosRepository;
    
    private readonly AppSettings _apiSetting;

    public AutenticacaoController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOptions<AppSettings> apiSetting, IUsuariosRepository usuariosRepository)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _usuariosRepository = usuariosRepository;
        _apiSetting = apiSetting.Value;
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if (!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signInManager.PasswordSignInAsync(usuarioLogin.Email, usuarioLogin.Password, false, false);

        if (!result.Succeeded)
        {
            AdicionarErro("Usuário ou Senha incorretos");
            return CustomResponse();
        }
        
        
        var userLogin = await GerarJwt(usuarioLogin.Email);

        return CustomResponse(userLogin);
    }


    private async Task<UsuarioRespostaLogin> GerarJwt(string login)
    {
        var user = await _userManager.FindByNameAsync(login);

        var claims = await _userManager.GetClaimsAsync(user);

        var identityClaims = await ObterClaimsUsuario(claims, user);

        var encodedToken = CodificarToken(identityClaims);

        return ObterRespostaToken(encodedToken, user, claims);
    }

    private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var domainUser = await _usuariosRepository.ObterPorEmail(user.Email);

        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim("userId", domainUser.Id.ToString()));
        claims.Add(new Claim("name", domainUser.Nome));
        claims.Add(new Claim("role", domainUser.TipoUsuario.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(),
            ClaimValueTypes.Integer64));

        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

    private string CodificarToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_apiSetting.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _apiSetting.Emissor,
            Audience = _apiSetting.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_apiSetting.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user,
        IEnumerable<Claim> claims)
    {
        return new UsuarioRespostaLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_apiSetting.ExpiracaoHoras).TotalSeconds,
            UsuarioToken = new UsuarioToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value }),
            }
        };
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
}