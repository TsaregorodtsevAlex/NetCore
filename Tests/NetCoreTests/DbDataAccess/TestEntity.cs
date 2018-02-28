using System.ComponentModel.DataAnnotations.Schema;
using NetCoreDataAccess.Interfaces;

namespace NetCoreTests.DbDataAccess
{
    public class TestEntity: IEntity
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
