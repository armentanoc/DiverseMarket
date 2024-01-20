using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;

namespace DiverseMarket.Backend.Services
{
    public static class OrderService
    {
        public static List<OrderBasicInfoDTO> GetAllOrdersByCustomerUserId(long userId)
        {

            throw new NotImplementedException();
        }

        public static List<OrderBasicInfoDTO> GetAllOrdersByCompanyUserId(long userId)
        {
            return SellingDB.GetAllOrdersByCompanyUserId(userId);
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
