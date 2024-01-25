using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.DTOs
{
    public class ProductOfferInsertDTO
    {
        public long CompanyUserId { get; }
        public string Name { get; }
        public string Description { get; }
        public int CategoryId { get; }
        public decimal Price { get; }
        public long Quantity { get; }

        public ProductOfferInsertDTO(long companyUserId, decimal price, long quantity, string name, int categoryid, string description)
        {
            CompanyUserId = companyUserId;
            Price = price;
            Quantity = quantity;
            Name = name;
            Description = description;
            CategoryId = categoryid;
        }
    }
 }  
 

