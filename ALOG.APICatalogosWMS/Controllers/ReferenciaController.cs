using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ALOG.APICatalogosWMS;
using ALOG.Modelos;

namespace ALOG.APICatalogosWMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReferenciaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReferenciaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Referencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Referencia>>> GetReferencias()
        {
            return await _context.Referencias.ToListAsync();
        }

        // GET: api/Referencia/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Referencia>> GetReferencia(int id)
        {
            var referencia = await _context.Referencias.FindAsync(id);

            if (referencia == null)
            {
                return NotFound();
            }

            return referencia;
        }

        // PUT: api/Referencia/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReferencia(int id, Referencia referencia)
        {
            if (id != referencia.Id)
            {
                return BadRequest();
            }

            _context.Entry(referencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReferenciaExists(id))
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

        // POST: api/Referencia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Referencia>> PostReferencia(Referencia referencia)
        {
            _context.Referencias.Add(referencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReferencia", new { id = referencia.Id }, referencia);
        }

        // DELETE: api/Referencia/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferencia(int id)
        {
            var referencia = await _context.Referencias.FindAsync(id);
            if (referencia == null)
            {
                return NotFound();
            }

            _context.Referencias.Remove(referencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReferenciaExists(int id)
        {
            return _context.Referencias.Any(e => e.Id == id);
        }
    }
}
