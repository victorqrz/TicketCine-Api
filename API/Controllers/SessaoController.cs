using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessoesController : ControllerBase
    {
        private readonly APIContext _context;

        public SessoesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Sessoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sessao>>> GetSessoes()
        {
            if (_context.Sessoes == null)
            {
                return NotFound();
            }

            return await _context.Sessoes
                .ToListAsync(); // Remover Includes se não houver relação de navegação
        }

        [HttpGet("{id}")] // Adicionando a rota para incluir o ID
        public async Task<ActionResult<Sessao>> GetSessao(int id)
        {
            if (_context.Sessoes == null)
            {
                return NotFound();
            }

            // Incluindo as entidades relacionadas
            var sessao = await _context.Sessoes
                .Include(s => s.Filme) // Incluindo a propriedade de navegação Filme
                .Include(s => s.Sala)  // Incluindo a propriedade de navegação Sala
                .FirstOrDefaultAsync(s => s.IdSessao == id); // Buscando a sessão pelo ID

            if (sessao == null)
            {
                return NotFound();
            }

            return sessao;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Operador, Gestor")]
        public async Task<IActionResult> PutSessao(int id, SessaoDTO sessaoDto)
        {

            // Verificar se o Filme existe pelo IdFilme
            var filmeExistente = await _context.Filmes.FindAsync(sessaoDto.IdFilme);
            if (filmeExistente == null)
            {
                return BadRequest("Filme não encontrado.");
            }

            // Verificar se a Sala existe pelo IdSala
            var salaExistente = await _context.Salas.FindAsync(sessaoDto.IdSala);
            if (salaExistente == null)
            {
                return BadRequest("Sala não encontrada.");
            }

            // Atualizar a sessão com os novos dados
            var sessao = await _context.Sessoes.FindAsync(id);
            if (sessao == null)
            {
                return NotFound();
            }

            sessao.HoraInicio = sessaoDto.HoraInicio;
            sessao.HoraFim = sessaoDto.HoraFim;
            sessao.Valor = sessaoDto.Valor;
            sessao.IdFilme = sessaoDto.IdFilme;
            sessao.IdSala = sessaoDto.IdSala;

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

        // POST: api/Sessoes
        // Ação permitida apenas para Operador e Gestor
        [HttpPost]
        [Authorize(Roles = "Operador, Gestor")]
        public async Task<ActionResult<Sessao>> PostSessao(SessaoDTO sessaoDto)
        {
            if (_context.Sessoes == null)
            {
                return Problem("Entity set 'APIContext.Sessoes' is null.");
            }

            // Verificar se o Filme existe pelo IdFilme
            var filmeExistente = await _context.Filmes.FindAsync(sessaoDto.IdFilme);
            if (filmeExistente == null)
            {
                return BadRequest("Filme não encontrado.");
            }

            // Verificar se a Sala existe pelo IdSala
            var salaExistente = await _context.Salas.FindAsync(sessaoDto.IdSala);
            if (salaExistente == null)
            {
                return BadRequest("Sala não encontrada.");
            }

            // Criar nova sessão com IDs já existentes
            var sessao = new Sessao
            {
                HoraInicio = sessaoDto.HoraInicio,
                HoraFim = sessaoDto.HoraFim,
                Valor = sessaoDto.Valor,
                IdFilme = sessaoDto.IdFilme,
                IdSala = sessaoDto.IdSala
            };

            _context.Sessoes.Add(sessao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSessao", new { id = sessao.IdSessao }, sessao);
        }

        // DELETE: api/Sessoes/5
        // Ação permitida apenas para Operador e Gestor
        [HttpDelete("{id}")]
        [Authorize(Roles = "Operador, Gestor")]
        public async Task<IActionResult> DeleteSessao(int id)
        {
            if (_context.Sessoes == null)
            {
                return NotFound();
            }

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
            return (_context.Sessoes?.Any(e => e.IdSessao == id)).GetValueOrDefault();
        }
    }
}
