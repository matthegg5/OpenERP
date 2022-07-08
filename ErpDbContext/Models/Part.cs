using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.Models
{
    public partial class Part
    {
        public Part()
        {
            PartRevs = new HashSet<PartRev>();
            SalesOrderDtls = new HashSet<SalesOrderDtl>();
        }

        public string CompanyId { get; set; } = null!;
        public string PartNum { get; set; } = null!;
        public string PartDescription { get; set; } = null!;
        public bool SerialTracked { get; set; }
        public string DefaultUomcode { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<PartRev> PartRevs { get; set; }
        public virtual ICollection<SalesOrderDtl> SalesOrderDtls { get; set; }
    }
}
