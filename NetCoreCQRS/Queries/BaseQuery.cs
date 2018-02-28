using NetCoreDataAccess.UnitOfWork;
using NetCoreDI;

namespace NetCoreCQRS.Queries
{
    public class BaseQuery
    {
        private IUnitOfWork _unitOfWork;

        protected IUnitOfWork Uow => _unitOfWork ?? (_unitOfWork = AmbientContext.Current.UnitOfWork);
    }
}
