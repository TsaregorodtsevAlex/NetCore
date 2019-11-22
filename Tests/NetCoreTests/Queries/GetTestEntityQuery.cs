﻿using System.Collections.Generic;
using System.Linq;
using NetCoreCQRS.Queries;
using NetCoreTests.DbDataAccess;

namespace NetCoreTests.Queries
{
    public class GetTestEntityQuery : BaseQuery
    {
        public List<TestEntity> Execute()
        {
            var testRepository = GetRepository<TestEntity>();
            return testRepository.GetAll().ToList();
        }
    }
}