using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Infrastructure
{

    public class PartRepository : GenericRepository<Part>
    {
        private readonly OpenERPContext _context;

        public PartRepository(OpenERPContext context) : base(context)
        {
            this._context = context;
        }

        public override Part Update(Part entity)
        {
            var part = _context.Parts.Single(p => p.CompanyId == entity.CompanyId && p.PartNum == entity.PartNum);

            part.PartDescription = entity.PartDescription;

            return base.Update(part);
        }


    }

}