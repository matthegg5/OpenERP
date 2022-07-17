using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.DataModel
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
        public string OurUom { get; set; } = null!;
        public string SupplierUom { get; set; } = null!;
        public string CostElement { get; set; } = null!;
        public string CostCentre { get; set; } = null!;
        public string InternalOrder { get; set; } = null!;
        public DateTime? RequiredDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public string LastChangeUser { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;
        public virtual Uom Uom { get; set; } = null!;
        public virtual Uom UomNavigation { get; set; } = null!;
    }
}
