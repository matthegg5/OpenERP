using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.DataModel
{
    public partial class Customer
    {
        public Customer()
        {
            SalesOrderHeds = new HashSet<SalesOrderHed>();
        }

        public string CompanyId { get; set; } = null!;
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;
        public virtual ICollection<SalesOrderHed> SalesOrderHeds { get; set; }
    }
}
