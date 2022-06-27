using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string LoginId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string AuthKey { get; set; } = null!;
        public bool UserDisabled { get; set; }
        public string CompanyList { get; set; } = null!;
        public string Ssodomain { get; set; } = null!;
        public string Ssouser { get; set; } = null!;
    }
}
