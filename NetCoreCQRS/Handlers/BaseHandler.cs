using NetCoreDI;
using NetCoreDataAccess.UnitOfWork;

namespace NetCoreCQRS.Handlers
{
    public class BaseHandler
    {
		private IUnitOfWork _unitOfWork;

		protected IUnitOfWork Uow => _unitOfWork ?? (_unitOfWork = AmbientContext.Current.UnitOfWork);
	}
}
