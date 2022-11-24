using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using touristmgmApi.DataModel.ViewModel;
using touristmgmApi.BusinessLayer;
using Microsoft.Extensions.Logging;

namespace touristmgmApi.Controllers
{
    [Route("api/v1/branch/")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public static List<AddBranchModel> Profiles { get; set; }
        private ITouristBusiness _itb;

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ITouristBusiness itb, ILogger<EmployeeController> logger)
        {
            _itb = itb;
            _logger = logger;
            Profiles = new List<AddBranchModel>();
            Profiles.Add(new AddBranchModel() { BranchName = "Andaman",  Contact = "9923412345", EmailId = "eamil@gmail.com" });
            Profiles.Add(new AddBranchModel() { BranchName = "Singapore", Contact = "9923412345", EmailId = "fmamil@gmail.com" });
        }


        [HttpGet("getProfiles")]
        public async Task<List<AddBranchModel>> GetProfiles()
        {
            return await _itb.GetBranches();
        }

        [HttpGet("getProfile/{id}")]
        public async Task<AddBranchModel> GetBranch(string id)
        {
            return await _itb.GetBranchById(id);
        }

        [HttpPost]
        [Route("add-places")]
        public async Task<AddBranchModel> AddProfile([FromBody] AddBranchModel profile)
        {
            return await _itb.AddBranch(profile);
        }

        [HttpPost]
        [Route("update-tariff/{branchId}")]
        public async Task<AddBranchModel> UpdateProfile([FromBody] AddBranchModel profile)
        {
            return await _itb.UpdateBranch(profile);
        }
    }
}
