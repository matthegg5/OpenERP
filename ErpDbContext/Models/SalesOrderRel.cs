using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class SalesOrderRel
    {
        public string CompanyId { get; set; } = null!;
        public int SalesOrderNum { get; set; }
        public int SalesOrderLineNum { get; set; }
        public int SalesOrderRelNum { get; set; }
        public decimal ReleaseQty { get; set; }
        public DateTime? RequiredByDate { get; set; }
    }
}
