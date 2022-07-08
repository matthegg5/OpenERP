using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.Models
{
    public partial class PartRev
    {
        public string CompanyId { get; set; } = null!;
        public string PartNum { get; set; } = null!;
        public string PartRevNum { get; set; } = null!;
        public string PartRevDesc { get; set; } = null!;
        public bool Approved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public Guid ApprovedUser { get; set; }

        public virtual Company Company { get; set; } = null!;
        public virtual Part Part { get; set; } = null!;
    }
}
