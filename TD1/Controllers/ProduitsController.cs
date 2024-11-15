using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.DataManager;
using TD1.Models.EntityFramework;
using TD1.Models.Simple;

namespace TD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProduitsController : ControllerBase
    {
        private readonly ProduitManager _context;

        [ActivatorUtilitiesConstructor]
        public ProduitsController(ProduitManager manager)
        {
            _context = manager;
        }
        public ProduitsController()
        {

        }

        // GET: api/Produits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produit>>> GetProduits()
        {
            return await _context.GetAll();
        }

        // GET: api/Produits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produit>> GetProduit(int id)
        {
            var produit = await _context.GetById(id);

            if (produit == null)
            {
                return NotFound();
            }

            return produit;
        }

        // PUT: api/Produits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduit(int id, ProduitSimple produit)
        {
            Produit nouveauProduit = new()
            {
                Idproduit = produit.Idproduit,
                Nomproduit = produit.NomProduit,
                Description = produit.Description,
                Nomphoto = produit.Nomphoto,
                Uriphoto = produit.Uriphoto,
                Stockreel = produit.Stockreel,
                Stockmin = produit.Stockmin,
                Stockmax = produit.Stockmax,
                Idmarque = produit.IdMarque,
                Idtypeproduit = produit.IdTypeproduit,
            };

            var produitToUpdate = await _context.GetById(id);
            if (produitToUpdate.Value == null)
            {
                return NotFound();
            }

            await _context.Update(produitToUpdate.Value, nouveauProduit);
            return NoContent();
        }

        // POST: api/Produits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostProduit(ProduitSimple produit)
        {
            var nouveauProduit = new Produit
            {
                Idproduit = produit.Idproduit,
                Nomproduit = produit.NomProduit,
                Description = produit.Description,
                Nomphoto = produit.Nomphoto,
                Uriphoto = produit.Uriphoto,
                Stockreel = produit.Stockreel,
                Stockmin = produit.Stockmin,
                Stockmax = produit.Stockmax,
                Idmarque = produit.IdMarque,
                Idtypeproduit = produit.IdTypeproduit,
            };

            await _context.Add(nouveauProduit);
            return CreatedAtAction("GetProduit", new { id = nouveauProduit.Idproduit }, nouveauProduit);
        }

        // DELETE: api/Produits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduit(int id)
        {
            var produit = await _context.GetById(id);
            if (produit.Value == null)
            {
                return NotFound();
            }

            await _context.Delete(produit.Value);
            return NoContent();
        }
    }
}
