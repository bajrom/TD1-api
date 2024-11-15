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
    public class TypeProduitsController : ControllerBase
    {
        private readonly IDataRepository<TypeProduit> DataTypeProduit;

        public TypeProduitsController(IDataRepository<TypeProduit> context)
        {
            DataTypeProduit = context;
        }

        // GET: api/TypeProduits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetTypeProduits()
        {
            return await DataTypeProduit.GetAll();
        }

        // GET: api/TypeProduits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeProduit>> GetTypeProduit(int id)
        {
            var typeProduit = await DataTypeProduit.GetById(id);
            if (typeProduit == null)
            {
                return NotFound();
            }

            return typeProduit;
        }

        // PUT: api/TypeProduits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeProduit(int id, String nomTypeProduit)
        {
            TypeProduit typeproduit = new TypeProduit { Idtypeproduit = id, Nomtypeproduit = nomTypeProduit };

            var typeproduitToUpdate = await DataTypeProduit.GetById(id);

            try
            {
                await DataTypeProduit.Update(typeproduitToUpdate.Value, typeproduit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (typeproduitToUpdate == null)
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

        // POST: api/TypeProduits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TypeProduit>> PostTypeProduit(String nomTypeproduit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TypeProduit typeProduit = new TypeProduit { Nomtypeproduit = nomTypeproduit };
            await DataTypeProduit.Add(typeProduit);
            // _context.Marques.AddAsync(marque);

            return CreatedAtAction("GetTypeProduit", new { id = typeProduit.Idtypeproduit }, typeProduit);
        }

        // DELETE: api/TypeProduits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeProduit(int id)
        {
            var typeProduit = await DataTypeProduit.GetById(id);
            if (typeProduit == null)
            {
                return NotFound();
            }

            await DataTypeProduit.Delete(typeProduit.Value);

            return NoContent();
        }
    }
}
