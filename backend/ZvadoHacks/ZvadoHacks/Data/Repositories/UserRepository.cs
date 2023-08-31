using ZvadoHacks.Data.Entities;

namespace ZvadoHacks.Data.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
