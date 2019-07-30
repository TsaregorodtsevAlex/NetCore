using FluentAssertions;
using NetCoreDataAccess.Externsions;
using NetCoreDataAccessTests.Specifications;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System;
using NetCoreDataAccess.BaseRequests;
using NetCoreDataAccess.BaseResponses;
using NetCoreTests.Specifications;

namespace NetCoreDataAccessTests
{
    [TestFixture]
    public class DbQueriesExtensionsTests
    {
        private List<SpectificationTestEntity> SpectificationTestEntityList
        {
            get
            {
                return new List<SpectificationTestEntity>
                {
                    SpectificationTestEntity.Create(1,"1"),
                    SpectificationTestEntity.Create(3,"3"),
                    SpectificationTestEntity.Create(2,"2"),
                };
            }
        }

        [Test]
        public void DbQueriesExtensions_ApplySpecificationShouldBeImplementOnlyClass_ReturnsException()
        {
            var result = new EmptySpectification<SpectificationTestEntity>();

            result.Should().BeAssignableTo<EmptySpectification<SpectificationTestEntity>>();
        }

        [Test]
        public void DbQueriesExtensions_ApplySpecification_ReturnsAllList()
        {
            var emptySpectification = new EmptySpectification<SpectificationTestEntity>();

            var result = SpectificationTestEntityList.AsQueryable()
                .ApplySpecification(emptySpectification)
                .ToList();

            result.Should().HaveCount(SpectificationTestEntityList.Count);
        }

        [TestCase(true)]
        [TestCase(false)]
        [Test]
        public void DbQueriesExtensions_ApplySpecificationIf_ReturnsExpectedResultCount(bool predicate)
        {
            var textEqualsSpecification = new TextEqualsSpecification();

            var result = SpectificationTestEntityList.AsQueryable()
                .ApplySpecificationIf(textEqualsSpecification, () => predicate)
                .ToList();

            var shouldCount = SpectificationTestEntityList.Count();

            if (predicate == true)
            {
                shouldCount = SpectificationTestEntityList.Where(r => r.Id == 1).Count();
            }

            result.Should().HaveCount(shouldCount);
        }


        [TestCase("1")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("3bg63v5cw4y6buvcuvhy6yubeyxmse6nbv")]
        [Test]
        public void DbQueriesExtensions_WhereIfWithTruePredicate_ReturnsExpectedResultCount(string code)
        {
            var result = SpectificationTestEntityList.AsQueryable()
                .WhereIf((r) => r.Code == code, () => true)
                .ToList();

            var shouldResultCount = SpectificationTestEntityList
                   .Where(r => r.Code == code)
                   .ToList()
                   .Count;

            result.Should().HaveCount(shouldResultCount);
        }

        [TestCase("1")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("1")]
        [Test]
        public void DbQueriesExtensions_WhereIfWithFalsePredicate_ReturnsExpectedResultCount(string code)
        {
            var result = SpectificationTestEntityList.AsQueryable()
                .WhereIf((r) => r.Code == code, () => false)
                .ToList();

            result.Should().HaveCount(SpectificationTestEntityList.Count);
        }


        [Category("Method ApplyPagedListRequest")]
        [Test]
        public void DbQueriesExtensions_ApplyPagedListRequestWithNullListResponseBase_ReturnsException()
        {
            IQueryable<SpectificationTestEntity> result = null;
            try
            {
                result = SpectificationTestEntityList.AsQueryable().ApplyPagedListRequest(new PagedListRequest(), null);
            }
            catch (Exception ex)
            {
                ex.Should().BeAssignableTo<NullReferenceException>();
            }

            result.Should().BeNull();
        }

        [Category("Method ApplyPagedListRequest")]
        [Test]
        public void DbQueriesExtensions_ApplyPagedListRequestWithNullPagedListRequest_ReturnsException()
        {
            IQueryable<SpectificationTestEntity> result = null;
            try
            {
                result = SpectificationTestEntityList.AsQueryable().ApplyPagedListRequest(null, new ListResponseBase());
            }
            catch (Exception ex)
            {
                ex.Should().BeAssignableTo<NullReferenceException>();

            }

            result.Should().BeNull();
        }

        [Category("Method ApplyPagedListRequest")]
        [Test]
        public void DbQueriesExtensions_ApplyPagedListRequest_ReturnsEmptyList()
        {
            var result = SpectificationTestEntityList.AsQueryable()
                .ApplyPagedListRequest(new PagedListRequest(), new ListResponseBase())
                .ToList();

            result.Should().HaveCount(0);
        }

        [Category("Method ApplyPagedListRequest")]
        [Test]
        public void DbQueriesExtensions_ApplyPagedListRequestEmptyQuery_ReturnsException()
        {
            IQueryable<int> list = null;
            IQueryable<int> result = null;
            try
            {
                result = list.ApplyPagedListRequest(new PagedListRequest(), new ListResponseBase());
            }
            catch (Exception ex)
            {
                ex.Should().As<ArgumentException>();
            }

            result.Should().BeNull();
        }


        [Category("Method ApplyPagedListRequest")]
        [TestCase(0,0)]
        [TestCase(1, 1)]
        [TestCase(1, 10)]
        [TestCase(null, 20)]
        [TestCase(null, null)]
        [TestCase(1, null)]
        [Test]
        public void DbQueriesExtensions_ApplyPagedListRequestWithDifferentPagingParameters_ReturnsExpectedItemsCount(int skip, int take)
        {
            var pagedListRequest = new PagedListRequest
            {
                Skip = skip,
                Take = take
            };

            var result = SpectificationTestEntityList.AsQueryable()
                .ApplyPagedListRequest(pagedListRequest, new ListResponseBase())
                .ToList();


            var shouldBeCount = SpectificationTestEntityList
                .Skip(skip)
                .Take(take)
                .Count();

            result.Should().HaveCount(shouldBeCount);
        }
    }
}
