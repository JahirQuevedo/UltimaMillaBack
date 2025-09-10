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
    public class BarcoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BarcoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Barco
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barco>>> GetBarcos()
        {
            return await _context.Barcos.ToListAsync();
        }

        // GET: api/Barco/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Barco>> GetBarco(int id)
        {
            var barco = await _context.Barcos.FindAsync(id);

            if (barco == null)
            {
                return NotFound();
            }

            return barco;
        }

        // PUT: api/Barco/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarco(int id, Barco barco)
        {
            if (id != barco.Id)
            {
                return BadRequest();
            }

            _context.Entry(barco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarcoExists(id))
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

        // POST: api/Barco
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Barco>> PostBarco(Barco barco)
        {
            _context.Barcos.Add(barco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBarco", new { id = barco.Id }, barco);
        }

        // DELETE: api/Barco/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarco(int id)
        {
            var barco = await _context.Barcos.FindAsync(id);
            if (barco == null)
            {
                return NotFound();
            }

            _context.Barcos.Remove(barco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BarcoExists(int id)
        {
            return _context.Barcos.Any(e => e.Id == id);
        }
    }
}
