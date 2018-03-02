using Microsoft.EntityFrameworkCore;
using NetCoreDataAccess;

namespace NetCoreTests.DbDataAccess
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TestEntity> TestEntities { get; set; }
    }
}
