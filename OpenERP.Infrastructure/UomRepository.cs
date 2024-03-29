using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Infrastructure
{

    public class UomRepository : GenericRepository<Uom>
    {
        private readonly OpenERPContext _context;
        public UomRepository(OpenERPContext context) : base(context)
        {
            this._context = context;
        }
    }
}