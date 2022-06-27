using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class Part
    {
        public string CompanyId { get; set; } = null!;
        public string PartNum { get; set; } = null!;
        public string PartDescription { get; set; } = null!;
        public bool SerialTracked { get; set; }
        public string DefaultUomcode { get; set; } = null!;
    }
}
