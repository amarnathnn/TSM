using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using touristmgmApi.DataModel;
namespace touristmgmApi.DataModel.ViewModel
{
    public class AddBranchModel
    {
        public string BranchId { get; set; }
        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string BranchName { get; set; }
      
        [Required]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [Range(1111111111,9999999999)]
        public string Contact { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        public List<Place> Places { get; set; }

        public AddBranchModel()
        {
            Places = new List<Place>();

        }
    }

    public class Place
    {
        public string PlaceID { get; set; }

        public string PlaceName { get; set; }

        [Required]
        [Range(0, 50000)]
        public int TariffFrom { get; set; }

        [Required]
        [Range(50000, 100000)]
        public int TariffTo { get; set; }
    }
}
