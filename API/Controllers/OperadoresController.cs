using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperadoresController : ControllerBase
    {
        private readonly APIContext _context;

        public OperadoresController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Operadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operador>>> GetOperadores()
        {
          if (_context.Operadores == null)
          {
              return NotFound();
          }
            return await _context.Operadores.ToListAsync();
        }

        // GET: api/Operadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operador>> GetOperador(string id)
        {
          if (_context.Operadores == null)
          {
              return NotFound();
          }
            var operador = await _context.Operadores.FindAsync(id);

            if (operador == null)
            {
                return NotFound();
            }

            return operador;
        }

        // PUT: api/Operadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperador(string id, Operador operador)
        {
            if (id != operador.CPF)
            {
                return BadRequest();
            }

            _context.Entry(operador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperadorExists(id))
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

        // POST: api/Operadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Operador>> PostOperador(Operador operador)
        {
          if (_context.Operadores == null)
          {
              return Problem("Entity set 'APIContext.Operadores'  is null.");
          }
            _context.Operadores.Add(operador);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OperadorExists(operador.CPF))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOperador", new { id = operador.CPF }, operador);
        }

        // DELETE: api/Operadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperador(string id)
        {
            if (_context.Operadores == null)
            {
                return NotFound();
            }
            var operador = await _context.Operadores.FindAsync(id);
            if (operador == null)
            {
                return NotFound();
            }

            _context.Operadores.Remove(operador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperadorExists(string id)
        {
            return (_context.Operadores?.Any(e => e.CPF == id)).GetValueOrDefault();
        }
    }
}
