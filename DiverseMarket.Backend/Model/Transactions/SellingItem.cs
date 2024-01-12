using DiverseMarket.Backend.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Model.Transactions
{
    internal class SellingItem
    {
        public long Id { get; private set; }
        public long ProductOfferId { get; private set; }
        public long SellingId { get; private set; }
        public int Quantity { get; private set; }
        public double UnityPrice { get; private set; }
        public OrderStatus Status { get; private set; }

        public SellingItem(long id, long productOfferId, long sellingId, int quantity, double unityPrice, OrderStatus status)
        {
            Id = id;
            ProductOfferId = productOfferId;
            SellingId = sellingId;
            Quantity = quantity;
            UnityPrice = unityPrice;
            Status = status;
        }
    }
}
