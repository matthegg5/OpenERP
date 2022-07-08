using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.Models
{
    public partial class SalesOrderDtl
    {
        public SalesOrderDtl()
        {
            SalesOrderRels = new HashSet<SalesOrderRel>();
        }

        public string CompanyId { get; set; } = null!;
        public int SalesOrderNum { get; set; }
        public int SalesOrderLineNum { get; set; }
        public string PartNum { get; set; } = null!;
        public string LineDesc { get; set; } = null!;
        public decimal LineQty { get; set; }
        public string SalesUom { get; set; } = null!;
        public string SolineComments { get; set; } = null!;

        public virtual Part Part { get; set; } = null!;
        public virtual SalesOrderHed SalesOrderHed { get; set; } = null!;
        public virtual ICollection<SalesOrderRel> SalesOrderRels { get; set; }
    }
}
