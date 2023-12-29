using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Model.Transactions.Sellings.Selling
{
    public enum SellingStatus
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3,
        Canceled = 4
    }
}
