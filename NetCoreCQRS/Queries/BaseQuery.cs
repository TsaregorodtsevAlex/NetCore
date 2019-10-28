using NetCoreDataAccess.UnitOfWork;
using NetCoreDI;

namespace NetCoreCQRS.Queries
{
    public class BaseQuery
    {
        private IUnitOfWork _unitOfWork;

        protected IUnitOfWork Uow => _unitOfWork ?? (_unitOfWork = AmbientContext.Current.UnitOfWork);

        public void Clean()
        {
			// {a.kalinin} если использовать как scoped, то контекст оказывается уничтоженным
			//if (_unitOfWork != null)
			//{
			//	_unitOfWork.Dispose();
			//	_unitOfWork = null;
			//}
        }
    }

    public class BaseQuery<TContext>
    {
	    public TContext Context;

	    public void SetContext(TContext context)
	    {
		    Context = context;
	    }
    }
}
