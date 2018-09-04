using NUnit.Framework;
using FluentAssertions;
using NetCoreTests;
using NetCoreTests.DbDataAccess;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NetCoreDataAccess.Repository;
using System.Linq;

namespace NetCoreDataAccessTests
{
    [TestFixture]
    public class RepositoryTests : BaseTest
    {
        private TestDbContext Context = null;
        [SetUp]
        public void EachMethodSetUp()
        {
            ClearInMemoryDb();

            Context = (TestDbContext)ServiceProvider.GetService(typeof(DbContext));
            FillTestDataBaseData();
        }

        [Test]
        public void Repository_GetById_ReturnsExpectedEntity()
        {
            var repository = new Repository<TestEntity>(Context);
            var result = repository.GetById(1);

            var shouldBeResult = Context.Set<TestEntity>().ToList().Single(r => r.Id == 1);

            result.Should().BeEquivalentTo(shouldBeResult);
        }

        [Test]
        public void Repository_GetAll_ReturnsExpectedEntities()
        {
            var repository = new Repository<TestEntity>(Context);
            var result = repository.GetAll();

            var shouldBeResult = Context.Set<TestEntity>().AsEnumerable();

            result.Should().BeEquivalentTo(shouldBeResult);
        }

        [Test]
        public void Repository_Create_ReturnsSavedEntity()
        {
            var repository = new Repository<TestEntity>(Context);
            repository.Create(new TestEntity { Id = 10, Message = "message text"});
            Context.SaveChanges();

            var shouldBeResult = repository.GetById(10);

            shouldBeResult.Should().NotBeNull();
        }


        [Test]
        public void Repository_CreateRange_ReturnsSavedEntities()
        {
            var repository = new Repository<TestEntity>(Context);

            var testEntities = new List<TestEntity>
            {
                new TestEntity { Id = 10, Message = "message text" },
                new TestEntity { Id = 11, Message = "message text" }
            };

            repository.CreateRange(testEntities);

            Context.SaveChanges();

            var shouldBeResult = repository.AsQueryable().Where(r => r.Id == 10 || r.Id == 11);

            shouldBeResult.Should().NotBeNull();
        }


        private void FillTestDataBaseData()
        {
            Context.Add(new TestEntity { Id = 1, Message = "1m"});
            Context.Add(new TestEntity { Id = 2, Message = "2m" });
            Context.Add(new TestEntity { Id = 3, Message = "3m" });
            Context.Add(new TestEntity { Id = 4, Message = "4m" });
            Context.SaveChanges();
        }
    }
}
