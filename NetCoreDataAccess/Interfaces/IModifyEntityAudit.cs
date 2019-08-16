using System;

namespace NetCoreDataAccess.Interfaces
{
    public interface IModifyEntityAudit
    {
        DateTime UpdateDate { get; set; }        
    }
}