﻿using NetCoreDataAccess.UnitOfWork;
using NetCoreDI;

namespace NetCoreCQRS
{
    public class BaseCommand
    {
        private IUnitOfWork _unitOfWork;

        protected IUnitOfWork Uow => _unitOfWork ?? AmbientContext.Current.UnitOfWork;
    }
}