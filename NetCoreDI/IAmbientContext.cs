using NetCoreDataAccess.UnitOfWork;

namespace NetCoreDI
{
    public interface IAmbientContext
    {
        IUnitOfWork UnitOfWork { get; }
        TObject ResolveObject<TObject>();
    }
}
