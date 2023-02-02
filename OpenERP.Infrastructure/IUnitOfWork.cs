using OpenERP.ErpDbContext.DataModel;

namespace OpenERP.Infrastructure
{

    public interface IUnitOfWork
    {
        IRepository<Part> PartRepository { get; }
        IRepository<Uom> UomRepository { get; }
        IRepository<Company> CompanyRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}