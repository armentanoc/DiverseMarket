using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Backend.DTOs
{
    public class OrderDetailsDTO
    {
        public long UserId { get; private set; }
        public AddressDTO DeliveryAddress { get; private set; }
        public double TotalAmount { get; private set; }
        public List<OrderItemDTO> Items { get; private set; }

        public OrderDetailsDTO(long userId, AddressDTO deliveryAddress, List<OrderItemDTO> items)
        {
            UserId = userId;
            DeliveryAddress = deliveryAddress;
            Items = items;
            TotalAmount = 0;
            foreach (var item in Items)
            {
                TotalAmount += item.UnityPrice * item.UnityPrice;
            }
        }
    }
}
