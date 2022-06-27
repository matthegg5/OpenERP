using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class SalesOrderDtl
    {
        public string CompanyId { get; set; } = null!;
        public int SalesOrderNum { get; set; }
        public int SalesOrderLineNum { get; set; }
        public string PartNum { get; set; } = null!;
        public string LineDesc { get; set; } = null!;
        public decimal LineQty { get; set; }
        public string SalesUom { get; set; } = null!;
    }
}
