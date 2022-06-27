using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class Supplier
    {
        public string CompanyId { get; set; } = null!;
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;
        public bool Active { get; set; }
        public int AddressId { get; set; }
    }
}
