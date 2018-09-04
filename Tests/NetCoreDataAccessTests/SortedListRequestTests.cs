using NUnit.Framework;
using NetCoreDataAccess.BaseRequests;
using FluentAssertions;

namespace NetCoreDataAccessTests
{
    [TestFixture]
    public class SortedListRequestTests
    {
        [Test]
        public void SortedListRequest_GetAsSqlQueryStringWithAllParameters_ReturnsString()
        {
            var sortedListRequest = new SortedListRequest
            {
                FieldName = "TestFieldName",
                Direction = SortDirection.Descending
            };
            var result = sortedListRequest.AsSqlQueryString?.Trim();

            result.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void SortedListRequest_GetAsSqlQueryStringWithEmptyParameters_ReturnEmpty()
        {
            var sortedListRequest = new SortedListRequest();
            var result = sortedListRequest.AsSqlQueryString;

            result.Should().BeEmpty();
        }

        [TestCase("TestFieldName", null, "TestFieldName")]
        [TestCase("TestFieldName", SortDirection.Ascending, "TestFieldName")]
        [TestCase("TestFieldName", SortDirection.Descending, "TestFieldName DESC")]
        [TestCase(null, SortDirection.Ascending, "")]
        [TestCase(null, SortDirection.Descending, "")]
        [TestCase(null, null, "")]
        [Test]
        public void SortedListRequest_GetAsSqlQueryStringWithDifferentParameters_ReturnExpectedResult(string fieldName, SortDirection sortDirection, string shouldBeResult)
        {
            var sortedListRequest = new SortedListRequest
            {
                FieldName = fieldName,
                Direction = sortDirection
            };

            var result = sortedListRequest.AsSqlQueryString;

            result.Should().Be(shouldBeResult);
        }
    }
}
