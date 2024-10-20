using System;
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
    [Authorize(Roles = "Gerente, Operador")]
    public class SalasController : ControllerBase
    {
        private readonly APIContext _context;

        public SalasController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Salas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sala>>> GetSalas()
        {
          if (_context.Salas == null)
          {
              return NotFound();
          }
            return await _context.Salas.ToListAsync();
        }

        // GET: api/Salas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sala>> GetSala(int id)
        {
          if (_context.Salas == null)
          {
              return NotFound();
          }
            var sala = await _context.Salas.FindAsync(id);

            if (sala == null)
            {
                return NotFound();
            }

            return sala;
        }

// PUT: api/Salas/5
[HttpPut("{id}")]
public async Task<IActionResult> PutSala(int id, Sala sala)
{
    if (id != sala.IdSala)
    {
        return BadRequest();
    }

    // Verifica se existe outra sala com o mesmo número
    var salaExistente = await _context.Salas
        .FirstOrDefaultAsync(s => s.Numero == sala.Numero && s.IdSala != id);
    if (salaExistente != null)
    {
        return BadRequest("Já existe uma sala com esse número.");
    }

    // Busca a sala atual sem usar Include, pois QtdAssentos é uma propriedade simples
    var salaAtual = await _context.Salas.FirstOrDefaultAsync(s => s.IdSala == id);
    if (salaAtual == null)
    {
        return NotFound();
    }

    // Verifica se a quantidade de assentos foi alterada
    if (salaAtual.QtdAssentos != sala.QtdAssentos)
    {
        // Remove os assentos antigos da sala
        var assentosAntigos = await _context.Assentos.Where(a => a.Sala.IdSala == id).ToListAsync();
        _context.Assentos.RemoveRange(assentosAntigos);

        // Cria novos assentos de acordo com a nova quantidade
        var novosAssentos = new List<Assento>();
        for (int i = 0; i < sala.QtdAssentos; i++)
        {
            var novoAssento = new Assento
            {
                Identificador = Guid.NewGuid().ToString(), // Gera um identificador único
                Ocupado = false,
                Sala = salaAtual // Vincula os novos assentos à sala atual já rastreada
            };
            novosAssentos.Add(novoAssento);
        }

        _context.Assentos.AddRange(novosAssentos);
    }

    // Atualiza os dados da sala
    salaAtual.Numero = sala.Numero;
    salaAtual.QtdAssentos = sala.QtdAssentos;

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!SalaExists(id))
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



// POST: api/Salas
[HttpPost]
public async Task<ActionResult<Sala>> PostSala(Sala sala)
{
    if (_context.Salas == null)
    {
        return Problem("Entity set 'APIContext.Salas' is null.");
    }

    // Verifica se já existe uma sala com o mesmo número
    var salaExistente = await _context.Salas.FirstOrDefaultAsync(s => s.Numero == sala.Numero);
    if (salaExistente != null)
    {
        return BadRequest("Já existe uma sala com esse número.");
    }

    // Adiciona a sala ao banco de dados
    _context.Salas.Add(sala);
    await _context.SaveChangesAsync();

    // Cria os assentos associados à sala recém-criada
    var assentos = new List<Assento>();
    for (int i = 0; i < sala.QtdAssentos; i++)
    {
        var assento = new Assento
        {
            Identificador = Guid.NewGuid().ToString(), // Gera um identificador único
            Ocupado = false,
            Sala = sala
        };
        assentos.Add(assento);
    }

    // Adiciona os assentos ao banco de dados
    _context.Assentos.AddRange(assentos);
    await _context.SaveChangesAsync();

    return CreatedAtAction("GetSala", new { id = sala.IdSala }, sala);
}


        // DELETE: api/Salas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSala(int id)
        {
            if (_context.Salas == null)
            {
                return NotFound();
            }
            var sala = await _context.Salas.FindAsync(id);
            if (sala == null)
            {
                return NotFound();
            }

            _context.Salas.Remove(sala);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalaExists(int id)
        {
            return (_context.Salas?.Any(e => e.IdSala == id)).GetValueOrDefault();
        }
    }
}
