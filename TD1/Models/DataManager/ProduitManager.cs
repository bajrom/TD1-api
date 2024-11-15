using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.DTO;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Models.DataManager
{
    public class ProduitManager : IDataRepository<Produit>
    {
        private readonly ProduitDbContext? produitsDbContext;
        private readonly IMapper _mapper;

        public ProduitManager() { }

        public ProduitManager(ProduitDbContext context, IMapper mapper)
        {
            produitsDbContext = context;
            _mapper = mapper;
        }

        public async virtual Task<ActionResult<IEnumerable<Produit>>> GetAll()
        {
            var produits = await produitsDbContext.Produits.ToListAsync();
            var produitsDto = _mapper.Map<IEnumerable<ProduitDto>>(produits);
            return new OkObjectResult(produitsDto);
            //return await produitsDbContext.Produits.ToListAsync();
        }

        public async virtual Task<ActionResult<Produit>> GetById(int id)
        {
            var produits = await produitsDbContext.Produits.FirstOrDefaultAsync(u => u.Idproduit == id);
            var produitsDto = _mapper.Map<ProduitDetailDto>(produits);
            return new OkObjectResult(produitsDto);
            //return await produitsDbContext.Produits.FirstOrDefaultAsync(u => u.Idproduit == id);
        }

        public async virtual Task<ActionResult<Produit>> GetByString(string str)
        {
            return await produitsDbContext.Produits.FirstOrDefaultAsync(u => u.Nomproduit.ToUpper() == str.ToUpper());
        }

        public async virtual Task Add(Produit entity)
        {
            await produitsDbContext.Produits.AddAsync(entity);
            await produitsDbContext.SaveChangesAsync();
        }

        public async virtual Task Update(Produit produit, Produit entity)
        {
            produitsDbContext.Entry(produit).State = EntityState.Modified;
            produit.Idproduit = entity.Idproduit;
            produit.Nomproduit = entity.Nomproduit;
            produit.Description = entity.Description;
            produit.Nomphoto = entity.Nomphoto;
            produit.Uriphoto = entity.Uriphoto;
            produit.Stockreel = entity.Stockreel;
            produit.Stockmin = entity.Stockmin;
            produit.Stockmax = entity.Stockmax;
            produit.Idmarque = entity.Idmarque;
            produit.Idtypeproduit = entity.Idtypeproduit;
            await produitsDbContext.SaveChangesAsync();
        }

        public async virtual Task Delete(Produit produit)
        {
            produitsDbContext.Produits.Remove(produit);
            await produitsDbContext.SaveChangesAsync();
        }
    }
}
