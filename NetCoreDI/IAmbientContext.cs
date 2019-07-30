using NetCoreDataAccess.UnitOfWork;
using NetCoreLocalization;

namespace NetCoreDI
{
    public interface IAmbientContext
    {
        IUnitOfWork UnitOfWork { get; }
        ILocalizationService Localization { get; }
        IObjectResolver Resolver { get; }
    }
}
