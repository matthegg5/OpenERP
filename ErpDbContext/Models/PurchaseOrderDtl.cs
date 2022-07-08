using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.Models
{
    public partial class PurchaseOrderDtl
    {
        public string CompanyId { get; set; } = null!;
        public int PurchaseOrderNum { get; set; }
        public int PurchaseOrderLineNum { get; set; }
        public string PartNum { get; set; } = null!;
        public string LineDesc { get; set; } = null!;
        public decimal OurOrderQty { get; set; }
        public decimal SupplierOrderQty { get; set; }
        public string OurUomcode { get; set; } = null!;
        public string SupplierUomcode { get; set; } = null!;
        public DateTime? RequiredDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid LastChangeUser { get; set; }

        public virtual Company Company { get; set; } = null!;
    }
}
