﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiProductos.Context;
using ApiProductos.Models;

namespace ApiProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Getcategorias()
        {
            return await _context.categorias.ToListAsync();
        }

        // GET: api/Categoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _context.categorias.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        // PUT: api/Categoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (id != categoria.IdCategoria)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
        {
            _context.categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoria", new { id = categoria.IdCategoria }, categoria);
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return _context.categorias.Any(e => e.IdCategoria == id);
        }
    }
}
