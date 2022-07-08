using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.Models
{
    public partial class PurchaseOrderRel
    {
        public string CompanyId { get; set; } = null!;
        public int PurchaseOrderNum { get; set; }
        public int PurchaseOrderLineNum { get; set; }
        public int PurchaseOrderRelNum { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal OurOrderQty { get; set; }
        public decimal SupplierOrderQty { get; set; }
        public string OurUomcode { get; set; } = null!;
        public string SupplierUomcode { get; set; } = null!;
        public DateTime? LastChangeDate { get; set; }
        public Guid LastChangeUser { get; set; }

        public virtual Company Company { get; set; } = null!;
    }
}
