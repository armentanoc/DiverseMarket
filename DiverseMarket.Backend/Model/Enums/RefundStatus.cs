using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Model.Enums
{
    public enum RefundStatus
    {
        Accepted = 1,
        SellerAnalysis = 2,
        ModeratorAnalysis = 3,
        Denied = 4
    }
}
