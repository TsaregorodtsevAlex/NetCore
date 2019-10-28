using NetCoreDataAccess.UnitOfWork;
using NetCoreDI;

namespace NetCoreCQRS.Commands
{
    public class BaseCommand
    {
        private IUnitOfWork _unitOfWork;

        protected IUnitOfWork Uow => _unitOfWork ?? (_unitOfWork = AmbientContext.Current.UnitOfWork);
    }

    public class BaseCommand<TContext>
    {
	    public TContext Context;

	    public void SetContext(TContext context)
	    {
		    Context = context;
	    }
    }
}
