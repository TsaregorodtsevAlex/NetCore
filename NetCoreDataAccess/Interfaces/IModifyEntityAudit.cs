using System;

namespace NetCoreDataAccess.Interfaces
{
    public interface IModifyEntityAudit
    {
        DateTimeOffset DateUpdate { get; set; }        
    }
}