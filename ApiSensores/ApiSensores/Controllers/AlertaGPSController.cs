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
    public class AlertaGPSController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertaGPSController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AlertaGPS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlertaGPS>>> GetAlertaGPSS()
        {
            return await _context.AlertaGPSS.ToListAsync();
        }

        // GET: api/AlertaGPS/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlertaGPS>> GetAlertaGPS(int id)
        {
            var alertaGPS = await _context.AlertaGPSS.FindAsync(id);

            if (alertaGPS == null)
            {
                return NotFound();
            }

            return alertaGPS;
        }

        // PUT: api/AlertaGPS/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlertaGPS(int id, AlertaGPS alertaGPS)
        {
            if (id != alertaGPS.Id)
            {
                return BadRequest();
            }

            _context.Entry(alertaGPS).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlertaGPSExists(id))
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

        // POST: api/AlertaGPS
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AlertaGPS>> PostAlertaGPS(AlertaGPS alertaGPS)
        {
            _context.AlertaGPSS.Add(alertaGPS);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlertaGPS", new { id = alertaGPS.Id }, alertaGPS);
        }

        // DELETE: api/AlertaGPS/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlertaGPS(int id)
        {
            var alertaGPS = await _context.AlertaGPSS.FindAsync(id);
            if (alertaGPS == null)
            {
                return NotFound();
            }

            _context.AlertaGPSS.Remove(alertaGPS);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlertaGPSExists(int id)
        {
            return _context.AlertaGPSS.Any(e => e.Id == id);
        }
    }
}
