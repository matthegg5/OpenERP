using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Infrastructure
{

    public class UnitOfWork : IUnitOfWork
    {
        private readonly OpenERPContext context;

        public UnitOfWork(OpenERPContext context)
        {
            this.context = context;
        }

        private IRepository<Part> partRepository;
        public IRepository<Part> PartRepository
        {
            get
            {
                if(partRepository == null)
                {
                    partRepository = new PartRepository(context);
                }

                return partRepository;
            }

        }

        private IRepository<Company> companyRepository;
        public IRepository<Company> CompanyRepository
        {
            get
            {
                if(companyRepository == null)
                {
                    companyRepository = new CompanyRepository(context);
                }

                return companyRepository;
            }

        }


        private IRepository<Uom> uomRepository;
        public IRepository<Uom> UomRepository
        {
            get
            {
                if(uomRepository == null)
                {
                    uomRepository = new UomRepository(context);
                }

                return uomRepository;
            }
            
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}



