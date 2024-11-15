using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarquesController : ControllerBase
    {
        private readonly IDataRepository<Marque> DataMarque;

        public MarquesController(IDataRepository<Marque> context)
        {
            DataMarque = context;
        }

        // GET: api/Marques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marque>>> GetMarques()
        {
            return await DataMarque.GetAll();
        }

        // GET: api/Marques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Marque>> GetMarque(int id)
        {
            var marque = await DataMarque.GetById(id);

            if (marque == null)
            {
                return NotFound();
            }

            return marque;
        }

        // PUT: api/Marques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarque(int id, String nomMarque)
        {
            Marque marque = new Marque { Idmarque = id, Nommarque = nomMarque };

            var marqueToUpdate = await DataMarque.GetById(id);

            try
            {
                await DataMarque.Update(marqueToUpdate.Value, marque);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (marqueToUpdate == null)
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

        // POST: api/Marques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Marque>> PostMarque(String nomMarque)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Marque marque = new Marque { Nommarque = nomMarque };
            await DataMarque.Add(marque);
            // _context.Marques.AddAsync(marque);

            return CreatedAtAction("GetMarque", new { id = marque.Idmarque }, marque);
        }

        // DELETE: api/Marques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarque(int id)
        {
            var marque = await DataMarque.GetById(id);
            if (marque == null)
            {
                return NotFound();
            }

            await DataMarque.Delete(marque.Value);

            return NoContent();
        }
    }
}
