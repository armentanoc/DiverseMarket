using DiverseMarket.Backend.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.DTOs
{
    public class OrderBasicInfoDTO
    {
        public long Id { get; private set; }
        public DateTime Date { get; private set; }
        public OrderStatus Status { get; private set; }

        public double TotalAmount { get; private set; }

        public OrderBasicInfoDTO(long id, DateTime date, OrderStatus status, double totalAmount)
        {
            Id = id;
            Date = date;
            Status = status;
            TotalAmount = totalAmount;
        }
    }
}
