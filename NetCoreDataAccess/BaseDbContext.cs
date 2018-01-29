using Microsoft.EntityFrameworkCore;

namespace NetCoreDataAccess
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions options) : base(options)
        {

        }
    }

}
