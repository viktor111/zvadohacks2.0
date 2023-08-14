using ZvadoHacks.Data.Entities;

namespace ZvadoHacks.Data.Repositories
{
    public class BlogPostRepository : GenericRepository<BlogPost>
    {
        public BlogPostRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
