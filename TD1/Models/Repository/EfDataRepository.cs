/*using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

public class EfDataRepository<T> : IDataRepository<T> where T : class
    {
        private readonly ProduitDbContext _context;

        public EfDataRepository(ProduitDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(int id, T entity)
        {
            var existingEntity = await _context.Set<T>().FindAsync(id);
            if (existingEntity == null)
                return null;

            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return existingEntity;
        }

        public async Task<T> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
                return null;

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(predicate);
        }


        public async Task<T> GetByString(string text)
        {
            var entityType = typeof(T);
            var propertyInfo = entityType.GetProperty(text);

            if (propertyInfo == null || propertyInfo.PropertyType != typeof(string))
            {
                throw new ArgumentException("Property not found or not of type string.");
            }

            var query = _context.Set<T>().AsQueryable();

            query = query.Where(x => (string)propertyInfo.GetValue(x) == text);

            return await query.FirstOrDefaultAsync();
        }
}
*/