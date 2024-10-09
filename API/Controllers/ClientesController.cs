using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Services;
using API.DTO;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly APIContext _context;

        public ClientesController(APIContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

         // POST: api/Clientes
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ClienteDTO>> PostCliente(Cliente cliente)
        {
            // Validações básicas
            if (string.IsNullOrWhiteSpace(cliente.Email) || string.IsNullOrWhiteSpace(cliente.Senha))
            {
                return BadRequest("E-mail e senha são obrigatórios.");
            }

            // Verificar se já existe um cliente com o mesmo email
            var clienteExistente = await _context.Clientes
                .Where(c => c.Email.ToLower() == cliente.Email.ToLower())
                .FirstOrDefaultAsync();

            if (clienteExistente != null)
            {
                return Conflict("Já existe um cliente cadastrado com este e-mail.");
            }

            // Verificar se o CPF já está cadastrado
            var cpfExistente = await _context.Clientes
                .Where(c => c.CPF == cliente.CPF)
                .FirstOrDefaultAsync();

            if (cpfExistente != null)
            {
                return Conflict("Já existe um cliente cadastrado com este CPF.");
            }

            // Criptografar a senha antes de salvar
            cliente.Senha = CriptografiaService.GerarHashMd5(cliente.Senha);

            // Adicionar cliente ao banco de dados
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Criar o DTO para retornar sem a senha
            var clienteDTO = new ClienteDTO
            {
                IdCliente = cliente.IdCliente,
                CPF = cliente.CPF,
                Nome = cliente.Nome,
                DataNascimento = cliente.dataNascimento,
                Email = cliente.Email
            };

            // Retornar o cliente cadastrado com status 201 (Created) e sem a senha
            return CreatedAtAction("GetCliente", new { id = cliente.IdCliente }, clienteDTO);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
