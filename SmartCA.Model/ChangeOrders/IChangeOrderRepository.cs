using System;
using System.Collections.Generic;
using SmartCA.Model.NumberedProjectChildren;

namespace SmartCA.Model.ChangeOrders
{
    public interface IChangeOrderRepository : 
        INumberedProjectChildRepository<ChangeOrder>
    {
        decimal GetPreviousAuthorizedAmountFrom(ChangeOrder co);
        int GetPreviousTimeChangedTotalFrom(ChangeOrder co);
    }
}
