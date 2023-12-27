using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Model.Transactions
{
    public class SellingItem
    {
        // public Product Product {  get; private set; }
        // public  Company Company { get; private set; }
        public int Quantity { get; private set; }
        public Decimal Price { get; private set; }


        public SellingItem(int quantity, Decimal price)
        {
            Quantity = quantity;
            Price = price;
        }

        
        public void IncrementQuantity()
        {
            Quantity++;
        }

        public void DecrementQuantity()
        {
            if (Quantity > 0)
            {
                Quantity--;
            }
        }
    }
}


