using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.DataModel
{
    public partial class Supplier
    {
        public Supplier()
        {
            PurchaseOrderHeds = new HashSet<PurchaseOrderHed>();
        }

        public string CompanyId { get; set; } = null!;
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public bool Active { get; set; }
        public int AddressId { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<PurchaseOrderHed> PurchaseOrderHeds { get; set; }
    }
}
