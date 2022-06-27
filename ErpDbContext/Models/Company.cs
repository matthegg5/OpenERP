using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class Company
    {
        public string CompanyId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool Active { get; set; }
    }
}
