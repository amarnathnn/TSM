using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace touristmgmApi.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetTaskAsync();
        Task<T> FindByIdAsync(string id);
        Task SaveAsync(T entity);
        Task Delete(T entity);
    }
}
