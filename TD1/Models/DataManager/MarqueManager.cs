using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Models.DataManager
{
    public class MarqueManager : IDataRepository<Marque>
    {
        readonly ProduitDbContext? produitsDbContext;

        public MarqueManager() { }

        public MarqueManager(ProduitDbContext context)
        {
            produitsDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<Marque>>> GetAll()
        {
            return await produitsDbContext.Marques.ToListAsync();
        }

        public async Task<ActionResult<Marque>> GetById(int id)
        {
            return await produitsDbContext.Marques.FirstOrDefaultAsync(u => u.Idmarque == id);
        }

        public async Task<ActionResult<Marque>> GetByString(string str)
        {
            return await produitsDbContext.Marques.FirstOrDefaultAsync(u => u.Nommarque.ToUpper() == str.ToUpper());
        }

        public async Task Add(Marque entity)
        {
            await produitsDbContext.Marques.AddAsync(entity);
            await produitsDbContext.SaveChangesAsync();
        }

        public async Task Update(Marque marque, Marque entity)
        {
            produitsDbContext.Entry(marque).State = EntityState.Modified;
            marque.Idmarque = entity.Idmarque;
            marque.Nommarque = entity.Nommarque;
            await produitsDbContext.SaveChangesAsync();
        }

        public async Task Delete(Marque marque)
        {
            produitsDbContext.Marques.Remove(marque);
            await produitsDbContext.SaveChangesAsync();
        }
    }
}
