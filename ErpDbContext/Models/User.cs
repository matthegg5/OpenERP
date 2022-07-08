using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.Models
{
    public partial class User
    {
        public User()
        {
            PartRevs = new HashSet<PartRev>();
        }

        public Guid UserId { get; set; }
        public string LoginId { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string AuthKey { get; set; } = null!;
        public bool UserDisabled { get; set; }
        public string CompanyList { get; set; } = null!;
        public string Ssodomain { get; set; } = null!;
        public string Ssouser { get; set; } = null!;

        public virtual ICollection<PartRev> PartRevs { get; set; }
    }
}
