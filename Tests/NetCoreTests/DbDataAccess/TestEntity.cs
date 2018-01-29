using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreTests.DbDataAccess
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }

        [NotMapped]
        public static TestEntity Default => new TestEntity { Message = "Defautl message" };
    }
}
