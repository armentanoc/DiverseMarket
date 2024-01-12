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
    public static class OrderService
    {
        public static List<OrderBasicInfoDTO> GetAllOrdersByCustomerId(long customerId)
        {
            List<Selling> allOrdersByUserId = SellingDB.GetAllSellingByCustomerId(customerId);

            List<OrderBasicInfoDTO> allOrdersBasicInfo = new List<OrderBasicInfoDTO>(); 

            foreach(Selling order in allOrdersByUserId)
            {
                allOrdersBasicInfo.Add(new OrderBasicInfoDTO(order.Id, order.Date, order.Status, order.TotalAmount));
            }
            return allOrdersBasicInfo;
        }

        public static DateTime GetOrderDateById(long orderId)
        {
            throw new NotImplementedException();
        }

        public static OrderDetailsDTO GetOrderDetailsById(long orderId)
        {
            throw new NotImplementedException();
        }

        public static void SetOrderItemAsRecieved(long orderId, long itemId)
        {
            throw new NotImplementedException();
        }
    }
}
