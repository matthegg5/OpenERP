
using System.ComponentModel.DataAnnotations;

namespace OpenERP.ViewModels 
{

    public class SessionPropertiesViewModel
    {   
        [Required]
        public string CurrentCompanyID { get; set; }

    }

}
