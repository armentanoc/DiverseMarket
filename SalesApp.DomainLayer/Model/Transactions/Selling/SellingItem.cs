﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalesApp.DomainLayer.Model.Companies;
using SalesApp.DomainLayer.Model.Products;

namespace SalesApp.DomainLayer.Model.Transactions
{
    public class SellingItem
    {
        public int ProductId {  get; set; }
        public  int CompanyId { get;  set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }


        public SellingItem(int productId, int companyId, int quantity, Decimal price)
        {
            ProductId = productId;
            CompanyId = companyId;
            Quantity = quantity;
            Price = price;
        }

        public SellingItem()
        {
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

