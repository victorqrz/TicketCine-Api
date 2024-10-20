﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Gerente, Operador, Cliente")]
    public class FilmesController : ControllerBase
    {
        private readonly APIContext _context;

        public FilmesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Filmes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilmes()
        {
          if (_context.Filmes == null)
          {
              return NotFound();
          }
            return await _context.Filmes.ToListAsync();
        }

        // GET: api/Filmes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
          if (_context.Filmes == null)
          {
              return NotFound();
          }
            var filme = await _context.Filmes.FindAsync(id);

            if (filme == null)
            {
                return NotFound();
            }

            return filme;
        }

        // PUT: api/Filmes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilme(int id, Filme filme)
        {
            if (id != filme.IdFilme)
            {
                return BadRequest();
            }

            _context.Entry(filme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmeExists(id))
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

        // POST: api/Filmes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Filme>> PostFilme(Filme filme)
        {
          if (_context.Filmes == null)
          {
              return Problem("Entity set 'APIContext.Filmes'  is null.");
          }
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilme", new { id = filme.IdFilme }, filme);
        }

        // DELETE: api/Filmes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            if (_context.Filmes == null)
            {
                return NotFound();
            }
            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null)
            {
                return NotFound();
            }

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FilmeExists(int id)
        {
            return (_context.Filmes?.Any(e => e.IdFilme == id)).GetValueOrDefault();
        }
    }
}
