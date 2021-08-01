using Microsoft.EntityFrameworkCore;
using MyPoints.Identity.Data;
using MyPoints.Identity.Domain.Entities;
using MyPoints.Identity.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Repositories
{
    public class BaseRepository<T, U> : IRepository<T, U> where T : BaseEntity<U>
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InsertAsync(T obj)
        {
            await _context.Set<T>().AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task InsertRange(IEnumerable<T> obj)
        {
            await _context.Set<T>().AddRangeAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRange(IEnumerable<T> obj)
        {
            _context.Set<T>().UpdateRange(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T obj)
        {
            _context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(U id, Guid updatedUser)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            entity.IsExcluded = true;
            entity.UpdatedUser = updatedUser;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(IEnumerable<T> obj)
        {
            _context.Set<T>().RemoveRange(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<T>> Select()
        {
            return await _context.Set<T>()
                .Where(x => x.IsExcluded == false)
                .ToListAsync();
        }

        public async Task<T> Select(U id)
        {
            var entity = await _context.Set<T>()
                .Where(x => !x.IsExcluded)
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
            return entity;
        }
    }
}
