using SalesApp.Backend.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Backend.DTOs
{
    public class OrderItemDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Quantity { get; private set; }
        public string CompanyName { get; private set; }
        public double UnityPrice { get; private set; }
        public OrderStatus Status { get; private set; }

        public OrderItemDTO(long id, string name, string description, int quantity, string companyName, double unityPrice, OrderStatus status)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            CompanyName = companyName;
            UnityPrice = unityPrice;
            Status = status;
        }
    }
}
