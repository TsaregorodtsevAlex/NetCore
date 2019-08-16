using System;

namespace NetCoreDataAccess.Interfaces
{
    public interface ICreateEntityAudit
    {
        DateTime CreateDate { get; set; }
    }
}
