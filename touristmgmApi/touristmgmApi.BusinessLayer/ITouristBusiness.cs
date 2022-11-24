using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using touristmgmApi.DataModel;
using touristmgmApi.DataModel.ViewModel;
using touristmgmApi.Repository;
namespace touristmgmApi.BusinessLayer
{
    public interface ITouristBusiness
    {       
        Task<List<AddBranchModel>> GetBranches();
        Task<List<AddBranchModel>> GetFilteredBranches(SearchBranchModel searchTourist);
        Task<AddBranchModel> GetBranchById(string id);
        Task<AddBranchModel> UpdateBranch(AddBranchModel branch);
        Task<AddBranchModel> AddBranch(AddBranchModel branch);
        
    }
}
