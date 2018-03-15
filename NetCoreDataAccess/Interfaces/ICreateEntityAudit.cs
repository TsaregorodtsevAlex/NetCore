using System;

namespace NetCoreDataAccess.Interfaces
{
    public interface ICreateEntityAudit
    {
        DateTimeOffset DateCreate { get; set; }
    }
}
