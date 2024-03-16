using HelixLaserWorks.Core.Enumerations;
using HelixLaserWorks.Core.Models.Materials;
using System.ComponentModel.DataAnnotations;
using static HelixLaserWorks.Infrastructure.Data.Constants.DataConstants;

namespace HelixLaserWorks.Core.Models.Parts
{
    public class UserPartsQueryModel
    {
        public const int PartsPerPage = PartsPerPageDefaultCount;


        [Display(Name = "Part Material")]
        public int Material { get; set; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; set; } = string.Empty;

        public PartSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalPartsCount { get; set; }

        public ICollection<MaterialDropdownViewModel> Materials { get; set; } = null!;

        public ICollection<PartViewModel> Parts { get; set; } = new List<PartViewModel>();
    }
}
