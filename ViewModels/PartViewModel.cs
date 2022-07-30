using System.ComponentModel.DataAnnotations;
using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.ViewModels
{

    public class PartViewModel
    {

        [Required]
        public string CompanyId { get; set; }
        [Required]
        public string PartNum { get; set; }
        [Required]
        public string PartDescription { get; set; }
        public bool SerialTracked { get; set; }
        [Required]
        public string DefaultUOM { get; set; }

    }

}