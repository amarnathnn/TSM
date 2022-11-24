using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using touristmgmApi.Repository;

namespace touristmgmApi.Repository
{
    public abstract class Repository<T>:IRepository<T> where T : class
    {
        readonly IDynamoDBContext _context;

        public Repository(IDynamoDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetTaskAsync()
        {
            var conditions = new List<ScanCondition>();
            return await _context.ScanAsync<T>(conditions).GetRemainingAsync();
        }

        public async Task<T> FindByIdAsync(string id)
        {
            var result = await _context.QueryAsync<T>(id).GetRemainingAsync();
            return result.FirstOrDefault();
        }

        public async Task SaveAsync(T entity)
        {
            await _context.SaveAsync(entity);
        }
        public async Task Delete(T entity)
        {
            await _context.DeleteAsync<T>(entity);
        }

    }
}
