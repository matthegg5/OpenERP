using System;
using System.Collections.Generic;

namespace ErpDbContext.Models
{
    public partial class Customer
    {
        public string CompanyId { get; set; } = null!;
        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
    }
}
