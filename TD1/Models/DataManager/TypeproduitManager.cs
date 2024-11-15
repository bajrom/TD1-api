using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Models.DataManager
{
    public class TypeproduitManager : IDataRepository<TypeProduit>
    {
        readonly ProduitDbContext? produitsDbContext;

        public TypeproduitManager() { }

        public TypeproduitManager(ProduitDbContext context)
        {
            produitsDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<TypeProduit>>> GetAll()
        {
            return await produitsDbContext.TypeProduits.ToListAsync();
        }

        public async Task<ActionResult<TypeProduit>> GetById(int id)
        {
            return await produitsDbContext.TypeProduits.FirstOrDefaultAsync(u => u.Idtypeproduit == id);
        }

        public async Task<ActionResult<TypeProduit>> GetByString(string str)
        {
            return await produitsDbContext.TypeProduits.FirstOrDefaultAsync(u => u.Nomtypeproduit.ToUpper() == str.ToUpper());
        }

        public async Task Add(TypeProduit entity)
        {
            await produitsDbContext.TypeProduits.AddAsync(entity);
            await produitsDbContext.SaveChangesAsync();
        }

        public async Task Update(TypeProduit typeproduit, TypeProduit entity)
        {
            produitsDbContext.Entry(typeproduit).State = EntityState.Modified;
            typeproduit.Idtypeproduit = entity.Idtypeproduit;
            typeproduit.Nomtypeproduit = entity.Nomtypeproduit;
            await produitsDbContext.SaveChangesAsync();
        }

        public async Task Delete(TypeProduit typeproduit)
        {
            produitsDbContext.TypeProduits.Remove(typeproduit);
            await produitsDbContext.SaveChangesAsync();
        }
    }
}
