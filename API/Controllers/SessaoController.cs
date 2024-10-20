using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Gerente, Operador")]
    public class SessaoController : ControllerBase
    {
        private readonly APIContext _context;

        public SessaoController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Sessao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sessao>>> GetSessoes()
        {
            return await _context.Sessoes.Include(s => s.IdFilme).Include(s => s.IdSala).ToListAsync();
        }

        // GET: api/Sessao/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Sessao>> GetSessao(int id)
        {
            var sessao = await _context.Sessoes.FindAsync(id);

            if (sessao == null)
            {
                return NotFound();
            }

            return sessao;
        }

        // POST: api/Sessao
        public async Task<ActionResult<Sessao>> PostSessao(Sessao sessao)
        {
            // Buscar o filme e a sala existentes pelo Id
            var filme = await _context.Filmes.FindAsync(sessao.IdFilme); // Usando IdFilme como int
            var sala = await _context.Salas.FindAsync(sessao.IdSala);    // Usando IdSala como int

            // Verificar se o filme existe
            if (filme == null)
            {
                return NotFound(new { message = $"Filme com Id {sessao.IdFilme} não foi encontrado." });
            }

            // Verificar se a sala existe
            if (sala == null)
            {
                return NotFound(new { message = $"Sala com Id {sessao.IdSala} não foi encontrada." });
            }

            // Associar o filme e a sala à sessão
            sessao.Filme = filme;
            sessao.Sala = sala;

            // Adicionar a sessão ao contexto
            _context.Sessoes.Add(sessao);
            await _context.SaveChangesAsync();

            // Verificar se o operador logado é um "Operador"
            if (User.IsInRole("Operador"))
            {
                var cpfOperador = User.FindFirst("cpf")?.Value;
                if (cpfOperador == null)
                {
                    return Unauthorized(new { message = "CPF do operador não encontrado." });
                }

                var operadorSessao = new OperadorSessao
                {
                    CPF = cpfOperador,
                    IdSessao = sessao.IdSessao
                };

                _context.OperadorSessao.Add(operadorSessao);
                await _context.SaveChangesAsync();
            }

            // Retornar a sessão criada
            return CreatedAtAction(nameof(GetSessao), new { id = sessao.IdSessao }, sessao);
        }

        // PUT: api/Sessao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSessao(int id, Sessao sessao)
        {
            if (id != sessao.IdSessao)
            {
                return BadRequest();
            }

            _context.Entry(sessao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Sessao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSessao(int id)
        {
            var sessao = await _context.Sessoes.FindAsync(id);
            if (sessao == null)
            {
                return NotFound();
            }

            _context.Sessoes.Remove(sessao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SessaoExists(int id)
        {
            return _context.Sessoes.Any(e => e.IdSessao == id);
        }
    }
}
