using System;
using System.Linq;
using System.Collections.Generic;
using SmartCA.Model.Projects;
using SmartCA.Infrastructure;
using SmartCA.Infrastructure.RepositoryFramework;

namespace SmartCA.Model.ChangeOrders
{
    public static class ChangeOrderService
    {
        private static IChangeOrderRepository repository;
        private static IUnitOfWork unitOfWork;

        static ChangeOrderService()
        {
            ChangeOrderService.unitOfWork = new UnitOfWork();
            ChangeOrderService.repository =
                RepositoryFactory.GetRepository<IChangeOrderRepository,
                ChangeOrder>(ChangeOrderService.unitOfWork);
        }

        public static ChangeOrder GetChangeOrder(object changeOrderKey)
        {
            return ChangeOrderService.repository.FindBy(changeOrderKey);
        }

        public static IList<ChangeOrder> 
            GetChangeOrders(Project project)
        {
            return ChangeOrderService.repository.FindBy(project);
        }

        public static void SaveChangeOrder(ChangeOrder co)
        {
            ChangeOrderService.repository[co.Key] = co;
            ChangeOrderService.unitOfWork.Commit();
        }

        public static decimal GetPreviousAuthorizedAmountFrom(ChangeOrder co)
        {
            return 
                ChangeOrderService.repository.GetPreviousAuthorizedAmountFrom(co);
        }

        public static int GetPreviousTimeChangedTotalFrom(ChangeOrder co)
        {
            return 
                ChangeOrderService.repository.GetPreviousTimeChangedTotalFrom(co);
        }
    }
}
