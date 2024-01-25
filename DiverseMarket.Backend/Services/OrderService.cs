using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model;
//using DiverseMarket.Backend.Model;

namespace DiverseMarket.Backend.Services
{
    public static class OrderService
    {
        public static List<OrderBasicInfoDTO> GetAllOrdersByCustomerUserId(long userId)
        { 
            return SellingDB.GetAllOrdersByCustomerUserId(userId);
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

        public static OrderSpecificDetailsDTO GetOrderDetails(OrderBasicInfoDTO order)
        {

            if (order != null)
            {
                long userId = CompanyDB.GetUserIdByCompanyId(order.CompanyId);
                Address address = AddressDB.GetAddressByUserId(userId);
                User user = UserDB.GetUserById(userId);

                OrderSpecificDetailsDTO orderDetails = new OrderSpecificDetailsDTO(order, user, address);


                return orderDetails;
            }

            return null;
        }
    }
}
