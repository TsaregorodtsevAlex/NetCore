using Microsoft.EntityFrameworkCore;
using NetCoreDataAccess;

namespace NetCoreTests.DbDataAccess
{
    public class TestDbContext : BaseDbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
