using ZvadoHacks.Data.Entities;

namespace ZvadoHacks.Data.Repositories
{
    public class ScanRepository : GenericRepository<Scan>
    {
        public ScanRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
