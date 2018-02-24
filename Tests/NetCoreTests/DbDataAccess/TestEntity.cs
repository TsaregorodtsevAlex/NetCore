using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreTests.DbDataAccess
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string Message { get; set; }

        [NotMapped]
        public static TestEntity Default => new TestEntity { Message = "Default message" };

        public static TestEntity Constuct(string message)
        {
            return new TestEntity { Message = message };
        }
    }
}
