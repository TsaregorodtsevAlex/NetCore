using System;
using NetCoreCQRS.Queries;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Queries
{
    public class GetTestEntityFirstByEntityNameQuery: BaseQuery
    {
        public TestEntity Execute(string entityName)
        {
            var commonRepository = Uow.GetRepository();
            var entityClrType = commonRepository.GetEntityClrType(entityName);
            var entity = Activator.CreateInstance(entityClrType);
            commonRepository.Create(entity);
            Uow.SaveChanges();
            return (TestEntity)entity;
        }
    }
}