using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace OpenERP.ErpDbContext.Models {

    public class User : IdentityUser
    {
        //extensions to AspNetUser DB column defined here.
        [MaxLength(256)]
        public string CompanyList { get; set; }
    }

}