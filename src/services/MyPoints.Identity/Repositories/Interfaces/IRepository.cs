using MyPoints.Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPoints.Identity.Repositories.Interfaces
{
    public interface IRepository<T, U> where T : IBaseEntity<U>
    {
        Task InsertAsync(T obj);

        Task InsertRange(IEnumerable<T> obj);

        Task Update(T obj);

        Task UpdateRange(IEnumerable<T> obj);

        Task Delete(U id, Guid updatedUser);

        Task DeleteRange(IEnumerable<T> obj);

        Task<T> Select(U id);

        Task<IList<T>> Select();
    }
}
