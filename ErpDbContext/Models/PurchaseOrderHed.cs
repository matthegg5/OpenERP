using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class PurchaseOrderHed
    {
        public string CompanyId { get; set; } = null!;
        public int PurchaseOrderNum { get; set; }
        public int SupplierId { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CurrencyCode { get; set; } = null!;
        public int CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ApprovalStatus { get; set; } = null!;
        public DateTime? ApprovedDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public int LastChangeUser { get; set; }
    }
}
