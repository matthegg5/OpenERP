using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class SalesOrderHed
    {
        public string CompanyId { get; set; } = null!;
        public int SalesOrderNum { get; set; }
        public int CustomerId { get; set; }
        public int BillingAddressId { get; set; }
        public int ShippingAddressId { get; set; }
        public DateTime? CustomerRequiredDate { get; set; }
        public DateTime? SuggestedShipDate { get; set; }
        public bool OpenOrder { get; set; }
        public bool CancelledOrder { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string CustomerPonum { get; set; } = null!;
    }
}
