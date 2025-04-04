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
    public class AlertaNivelController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertaNivelController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AlertaNivel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaNivel>>> GetAlertaNiveles()
        {
            return await _context.AlertaNiveles.ToListAsync();
        }

        // GET: api/AlertaNivel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaNivel>> GetAlertaNivel(int id)
        {
            var alertaNivel = await _context.AlertaNiveles.FindAsync(id);

            if (alertaNivel == null)
            {
                return NotFound();
            }

            return alertaNivel;
        }

        // PUT: api/AlertaNivel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertaNivel(int id, AlertaNivel alertaNivel)
        {
            if (id != alertaNivel.Id)
            {
                return BadRequest();
            }

            _context.Entry(alertaNivel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertaNivelExists(id))
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

        // POST: api/AlertaNivel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlertaNivel>> PostAlertaNivel(AlertaNivel alertaNivel)
        {
            _context.AlertaNiveles.Add(alertaNivel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertaNivel", new { id = alertaNivel.Id }, alertaNivel);
        }

        // DELETE: api/AlertaNivel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertaNivel(int id)
        {
            var alertaNivel = await _context.AlertaNiveles.FindAsync(id);
            if (alertaNivel == null)
            {
                return NotFound();
            }

            _context.AlertaNiveles.Remove(alertaNivel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertaNivelExists(int id)
        {
            return _context.AlertaNiveles.Any(e => e.Id == id);
        }
    }
}
