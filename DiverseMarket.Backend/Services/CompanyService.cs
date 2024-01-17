using DiverseMarket.Backend.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Services
{
    public class CompanyService
    {
        public static string GetCompanyNameBySellingItemId(long sellingItemId)
        {
            long companyId = SellingItemDB.GetCompanyIdBySellingItemId(sellingItemId);

            return CompanyDB.GetCompanyNameById(companyId);
        }
    }
}
