using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Infrastructure
{

    public class CompanyRepository : GenericRepository<Company>
    {
        private readonly OpenERPContext _context;

        public CompanyRepository(OpenERPContext context) : base(context)
        {
            this._context = context;
        }

        public override Company Update(Company entity)
        {
            var company = _context.Companies.Single(p => p.CompanyId == entity.CompanyId);

            company.Name = entity.Name;

            return base.Update(company);
        }


    }

}