using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Win32;
using System.Net.Mime;
using API.DTO;
using System.Numerics;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly APIContext _context;
        private readonly TokenService _tokenService;

        public AuthController(APIContext context)
        {
            _context = context;
            _tokenService = new TokenService("secret-keysecret-keysecret-key", "ticket-cine", "cinema");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticacao([FromBody] AutenticacaoDTO autenticacaoDTO)
        {
            if(string.IsNullOrWhiteSpace(autenticacaoDTO.Email) || string.IsNullOrWhiteSpace(autenticacaoDTO.Senha))
            {
                return BadRequest("E-mail e/ou senha nulos");
            }

            Type tipoEntidade;
            switch (autenticacaoDTO.Entidade.ToLower())
            {
                case "operador":
                    tipoEntidade = typeof(Operador);
                    break;
                case "cliente":
                    tipoEntidade = typeof(Cliente);
                    break;
                //case "gestor":
                //    tipoEntidade = typeof(Gestor);
                //    break;
                default:
                    return BadRequest("Entidade inválida.");
            }

            //Buscar a entidade correspondente no context
            var dbSet = _context.GetType().GetProperty(tipoEntidade.Name + "es")?.GetValue(_context) as IQueryable<object>;

            if (dbSet == null)
            {
                return BadRequest("Entidade não encontrada no contexto.");
            }

            // Fazer a consulta com base no e-mail e senha criptografada
            var usuario = await dbSet
                .Where(e => EF.Property<string>(e, "Email").ToLower() == autenticacaoDTO.Email.ToLower()
                            && EF.Property<string>(e, "Senha") == CriptografiaService.GerarHashMd5(autenticacaoDTO.Senha))
                .SingleOrDefaultAsync();

            // Verificar se o usuário foi encontrado
            if (usuario == null)
            {
                return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });
            }

            string token = _tokenService.Builder(autenticacaoDTO.Email, autenticacaoDTO.Entidade);

            return Ok(new { Token = token });
        }
    }
}
