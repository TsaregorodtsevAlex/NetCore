using System;

namespace NetCoreDataAccess.Interfaces
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
        DateTime? DeleteDate { get; set; }
    }
}