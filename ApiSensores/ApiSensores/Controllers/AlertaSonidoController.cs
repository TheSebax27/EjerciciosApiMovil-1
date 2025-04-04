using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSensores.Context;
using ApiSensores.Models;

namespace ApiSensores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlertaSonidoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertaSonidoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AlertaSonido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaSonido>>> GetAlertaSonidos()
        {
            return await _context.AlertaSonidos.ToListAsync();
        }

        // GET: api/AlertaSonido/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaSonido>> GetAlertaSonido(int id)
        {
            var alertaSonido = await _context.AlertaSonidos.FindAsync(id);

            if (alertaSonido == null)
            {
                return NotFound();
            }

            return alertaSonido;
        }

        // PUT: api/AlertaSonido/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertaSonido(int id, AlertaSonido alertaSonido)
        {
            if (id != alertaSonido.Id)
            {
                return BadRequest();
            }

            _context.Entry(alertaSonido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertaSonidoExists(id))
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

        // POST: api/AlertaSonido
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlertaSonido>> PostAlertaSonido(AlertaSonido alertaSonido)
        {
            _context.AlertaSonidos.Add(alertaSonido);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertaSonido", new { id = alertaSonido.Id }, alertaSonido);
        }

        // DELETE: api/AlertaSonido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertaSonido(int id)
        {
            var alertaSonido = await _context.AlertaSonidos.FindAsync(id);
            if (alertaSonido == null)
            {
                return NotFound();
            }

            _context.AlertaSonidos.Remove(alertaSonido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertaSonidoExists(int id)
        {
            return _context.AlertaSonidos.Any(e => e.Id == id);
        }
    }
}
