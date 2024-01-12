using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Services
{
    public static class RefundService
    {
        public static List<RefundBasicInfoDTO> GetAllRefundsByCustomerId(long customerId)
        {
            List<Refund> allRefundsByUserId = RefundDB.GetAllRefundsByCustomerId(customerId);

            List<RefundBasicInfoDTO> refundsBasicInfo = new List<RefundBasicInfoDTO>();

            foreach (Refund refund in allRefundsByUserId)
            {
                refundsBasicInfo.Add(new RefundBasicInfoDTO(refund.Id, ProductDB.GetProductNameById(refund.ProductId),
                    CompanyDB.GetCompanyNameById(refund.CompanyId), refund.Status, refund.TotalAmount));
            }

            return refundsBasicInfo;
        }
    }
}
