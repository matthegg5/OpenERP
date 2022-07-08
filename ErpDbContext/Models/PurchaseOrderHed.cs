using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.Models
{
    public partial class PurchaseOrderHed
    {
        public string CompanyId { get; set; } = null!;
        public int PurchaseOrderNum { get; set; }
        public int SupplierId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CurrencyCode { get; set; } = null!;
        public Guid CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ApprovalStatus { get; set; } = null!;
        public DateTime? ApprovedDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public Guid LastChangeUser { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Supplier Supplier { get; set; } = null!;
    }
}
