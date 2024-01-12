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
            return SellingDB.GetOrderDateById(orderId);
        }

        public static OrderDetailsDTO GetOrderDetailsById(long orderId)
        {
            Selling selling = SellingDB.GetSellingById(orderId);
            List<SellingItem> sellingItems = SellingItemDB.GetAllItemsBySellingId(orderId);

            AddressDTO address = UserService.GetAddressByUserId(selling.CustomerId);

            List<OrderItemDTO> items = new List<OrderItemDTO>();
            
            foreach (var item in sellingItems)
            {
                items.Add(new OrderItemDTO(item.Id, ProductService.GetProductNameByProductOfferId(item.ProductOfferId),
                    ProductService.GetProductDescriptionByProductOfferId(item.ProductOfferId), item.Quantity, CompanyService.GetCompanyNameBySellingItemId(item.Id),
                    item.UnityPrice, item.Status)) ;
            }

            OrderDetailsDTO order = new OrderDetailsDTO(selling.Id, address, items);

            return order;
        }

        public static void SetOrderItemAsRecieved(long orderId, long itemId)
        {
            
        }
    }
}
