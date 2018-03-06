using System;
using System.Linq;
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
            dynamic entity = Activator.CreateInstance(entityClrType);
            commonRepository.Create(entity);
            Uow.SaveChanges();


            var res = commonRepository.AsQueriable(entity);

            return (TestEntity)entity;
        }
    }
}