using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.DataModel
{
    public partial class Uom
    {
        public Uom()
        {
            Parts = new HashSet<Part>();
            PurchaseOrderDtlUomNavigations = new HashSet<PurchaseOrderDtl>();
            PurchaseOrderDtlUoms = new HashSet<PurchaseOrderDtl>();
            SalesOrderDtls = new HashSet<SalesOrderDtl>();
        }

        public string CompanyId { get; set; } = null!;
        public string Uom1 { get; set; } = null!;
        public string Uomdescription { get; set; } = null!;
        public bool Active { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<Part> Parts { get; set; }
        public virtual ICollection<PurchaseOrderDtl> PurchaseOrderDtlUomNavigations { get; set; }
        public virtual ICollection<PurchaseOrderDtl> PurchaseOrderDtlUoms { get; set; }
        public virtual ICollection<SalesOrderDtl> SalesOrderDtls { get; set; }
    }
}
