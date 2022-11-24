using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using touristmgmApi.DataModel;
namespace touristmgmApi.Repository
{
    public interface ITouristRepository : IRepository<Branch>
    {
        Task UpdateTariff(Branch branch);
    }
    public class TouristRepository : Repository<Branch>, ITouristRepository
    {
        readonly IDynamoDBContext _context;
        const int AllowUpdateAfter = 10;
        public TouristRepository(IDynamoDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task UpdateTariff(Branch branch)
        {
            var existingEntity = await FindByIdAsync(branch.BranchId);

            if (existingEntity == null)
                throw new Exception($"Branch with Id '{branch.BranchId}' does not exist");

            DateTime dt = existingEntity.UpdatedDate.HasValue ? existingEntity.UpdatedDate.Value : existingEntity.CreatedDate;

            if (!ITariffUpdateAllow(branch))
                throw new Exception($"Minimum days '{AllowUpdateAfter}' required to update your profile");

            existingEntity.UpdatedDate = DateTime.Today;
            existingEntity.Places = branch.Places;

            await _context.SaveAsync(existingEntity);
        }

        private bool ITariffUpdateAllow(Branch branch)
        {
            if (branch.UpdatedDate.HasValue && IsModifiedWithinDay(branch.UpdatedDate.GetValueOrDefault()))
                return false;

            return IsModifiedWithinDay(branch.CreatedDate);
        }

        bool IsModifiedWithinDay(DateTime dt)
        {
            return dt.AddDays(AllowUpdateAfter) > DateTime.Today;
        }

    }
}
