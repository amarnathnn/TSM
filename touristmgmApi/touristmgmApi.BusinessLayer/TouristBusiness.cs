using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using touristmgmApi.DataModel;
using touristmgmApi.DataModel.ViewModel;
using touristmgmApi.Repository;
using place = touristmgmApi.DataModel.Place;
using placeViewModel = touristmgmApi.DataModel.ViewModel.Place;
namespace touristmgmApi.BusinessLayer
{
    public class TouristBusiness : ITouristBusiness
    {

        private ITouristRepository _itr;

        public TouristBusiness(ITouristRepository itr)
        {
            _itr = itr;
        }

        public async Task<List<AddBranchModel>> GetBranches()
        {
            List<AddBranchModel> lstBranches = new List<AddBranchModel>();
            var branches = await _itr.GetTaskAsync();

            lstBranches = branches.Select(x => new AddBranchModel
            {
                BranchId = x.BranchId,            
                BranchName = x.BranchName,
                EmailId = x.EmailId,
                Contact = x.Contact,
                Places = x.Places.Select(y => new placeViewModel
                {
                    PlaceID = y.PlaceID,
                    PlaceName = y.PlaceName,
                    TariffFrom = y.TariffFrom,
                    TariffTo = y.TariffTo
                }).ToList()
            }).ToList();

            return lstBranches;
        }
        public async Task<List<AddBranchModel>> GetFilteredBranches(SearchBranchModel searchBranch)
        {
            List<AddBranchModel> lstPersons = new List<AddBranchModel>();
            try
            {
                var branches = await _itr.GetTaskAsync();
                var filtered = branches.Where(x =>
                (x.BranchId == searchBranch.BranchId || string.IsNullOrEmpty(searchBranch.BranchId)) &&
                (x.BranchName == searchBranch.BranchName || string.IsNullOrEmpty(searchBranch.BranchName)) &&
                (x.Places.Any(y => y.PlaceName == searchBranch.Place) || string.IsNullOrEmpty(searchBranch.Place))
                ).ToList();

                lstPersons = filtered.Select(x => new AddBranchModel
                {
                    BranchId = x.BranchId,                   
                    BranchName = x.BranchName,
                    EmailId = x.EmailId,
                    Contact = x.Contact,
                    Places = x.Places.OrderByDescending(z => z.TariffTo).Select(y => new placeViewModel
                    {
                        PlaceID = y.PlaceID,
                        PlaceName = y.PlaceName,
                        TariffFrom = y.TariffFrom,
                        TariffTo = y.TariffTo
                    }).ToList()
                }).ToList();

                //lstPersons.Add(AKS);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstPersons;
        }
        public async Task<AddBranchModel> GetBranchById(string branchId)
        {
            AddBranchModel branch = new AddBranchModel();
            try
            {
                var branches = await _itr.FindByIdAsync(branchId);

                if (branches != null)
                {
                    branch.BranchId = branches.BranchId;
                    branch.BranchName = branches.BranchName;
                    branch.EmailId = branches.EmailId;
                    branch.Contact = branches.Contact;

                    branch.Places = branches.Places.
                                    Select(x => new placeViewModel
                                    {
                                        PlaceID = x.PlaceID,
                                        PlaceName = x.PlaceName,
                                        TariffFrom = x.TariffFrom,
                                        TariffTo = x.TariffTo
                                    }).ToList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return branch;
        }

        public async Task<AddBranchModel> UpdateBranch(AddBranchModel branch)
        {
            try
            {
                Branch _branch = new Branch();

                _branch.BranchId = branch.BranchId;
                _branch.Places = branch.Places.Select(x => new place
                {
                    PlaceID = x.PlaceID,
                    PlaceName = x.PlaceName,
                    TariffFrom = x.TariffFrom,
                    TariffTo = x.TariffTo
                }).ToList();

                await _itr.UpdateTariff(_branch);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return branch;

        }
        public async Task<AddBranchModel> AddBranch(AddBranchModel branch)
        {
            try
            {
                Branch _branch = new Branch();

                _branch.BranchId = string.IsNullOrEmpty(branch.BranchId) ? FormatId(branch.BranchId) : FormatId(branch.BranchId);
                _branch.BranchName = branch.BranchName;
                _branch.EmailId = branch.EmailId;
                _branch.Contact = branch.Contact;
                _branch.Places = branch.Places.Select(x => new place
                {
                    PlaceID = x.PlaceID,
                    PlaceName = x.PlaceName,
                    TariffFrom = x.TariffFrom,
                    TariffTo = x.TariffTo
                }).ToList();

                await _itr.SaveAsync(_branch);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return branch;

        }

        private string FormatId(string id)
        {
            var uid = id.ToUpperInvariant();
            return uid.StartsWith("BR") ? id : string.Format("BR{0}", uid);
        }
    }
}
