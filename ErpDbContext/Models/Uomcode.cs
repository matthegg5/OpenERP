using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class Uomcode
    {
        public string CompanyId { get; set; } = null!;
        public string Uomcode1 { get; set; } = null!;
        public string Uomdescription { get; set; } = null!;
        public bool Active { get; set; }
    }
}
