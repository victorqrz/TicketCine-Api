using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using API.DTO;

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
            _tokenService = new TokenService("secret-keysecret-key", "ticket-cine", "cinema");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticacao([FromBody] AutenticacaoDTO autenticacaoDTO)
        {
            if (string.IsNullOrWhiteSpace(autenticacaoDTO.Email) || string.IsNullOrWhiteSpace(autenticacaoDTO.Senha))
            {
                return BadRequest("E-mail e/ou senha nulos");
            }

            // Variável para armazenar o usuário encontrado
            object usuario = null;
            string role = string.Empty;  // Variável para armazenar a role do usuário

            // Identificar a entidade correspondente e buscar no respectivo DbSet
            switch (autenticacaoDTO.Entidade.ToLower())
            {
                case "operador":
                    usuario = await _context.Operadores
                        .Where(e => e.Email.ToLower() == autenticacaoDTO.Email.ToLower()
                                 && e.Senha == CriptografiaService.GerarHashMd5(autenticacaoDTO.Senha))
                        .SingleOrDefaultAsync();
                    role = "Operador";  // Defina a role apropriada
                    break;
                case "cliente":
                    usuario = await _context.Clientes
                        .Where(e => e.Email.ToLower() == autenticacaoDTO.Email.ToLower()
                                 && e.Senha == CriptografiaService.GerarHashMd5(autenticacaoDTO.Senha))
                        .SingleOrDefaultAsync();
                    role = "Cliente";  // Defina a role apropriada
                    break;
                case "gerente":
                    usuario = await _context.Gerentes
                        .Where(e => e.Email.ToLower() == autenticacaoDTO.Email.ToLower()
                                 && e.Senha == CriptografiaService.GerarHashMd5(autenticacaoDTO.Senha))
                        .SingleOrDefaultAsync();
                    role = "Gerente";  // Defina a role apropriada
                    break;
                default:
                    return BadRequest("Entidade inválida.");
            }

            // Verificar se o usuário foi encontrado
            if (usuario == null)
            {
                return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });
            }

            // Gerar o token de autenticação com a role correta
            string token = _tokenService.Builder(autenticacaoDTO.Email, role);

            return Ok(new { Token = token });
        }
    }
}
