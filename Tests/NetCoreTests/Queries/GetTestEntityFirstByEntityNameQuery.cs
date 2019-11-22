using System;
using System.Linq;
using NetCoreCQRS.Commands;
using NetCoreCQRS.Queries;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Queries
{
    public class GetTestEntityFirstByEntityNameQuery: BaseCommand
    {
        public TestEntity Execute(string entityName)
        {
            var commonRepository = GetRepository();
            var entityClrType = commonRepository.GetEntityClrType(entityName);
            dynamic entity = Activator.CreateInstance(entityClrType);
            commonRepository.Create(entity);
            SaveChanges();


            var res = commonRepository.AsQueriable(entity);

            return (TestEntity)entity;
        }
    }
}