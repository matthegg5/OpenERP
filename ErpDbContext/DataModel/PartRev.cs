using System;
using System.Collections.Generic;

namespace OpenERP.ErpDbContext.DataModel
{
    public partial class PartRev
    {
        public string CompanyId { get; set; } = null!;
        public string PartNum { get; set; } = null!;
        public string PartRevNum { get; set; } = null!;
        public string PartRevDesc { get; set; } = null!;
        public bool Approved { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedUser { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;
        public virtual Part Part { get; set; } = null!;
    }
}
