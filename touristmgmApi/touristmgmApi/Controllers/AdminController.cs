using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using touristmgmApi.BusinessLayer;
using touristmgmApi.DataModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace touristmgmApi.Controllers
{
    [Route("api/v1/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        public static List<AddBranchModel> Profiles { get; set; }
        private ITouristBusiness _itb;

        public AdminController(ITouristBusiness itb, ILogger<AdminController> logger)
        {
            _itb = itb;
            _logger = logger;

            Profiles = new List<AddBranchModel>();
            Profiles.Add(new AddBranchModel() { BranchName = "Andaman",  Contact = "9923412345", EmailId = "eamil@gmail.com" });
            Profiles.Add(new AddBranchModel() { BranchName = "Singapore", Contact = "9923412345", EmailId = "fmamil@gmail.com" });
        }

        [HttpPost]        
        public async Task<List<AddBranchModel>> GetBySearch(SearchBranchModel searchBranch)
        {
            return await _itb.GetFilteredBranches(searchBranch);
        }
    }
}
