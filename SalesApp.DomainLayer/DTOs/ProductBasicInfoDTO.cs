using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.DTOs
{
    public class ProductBasicInfoDTO
    {
        public long Id {  get; private set; }
        public string Name { get; private set; }  
        public string Description { get; private set; }
        public string Category { get; private set; }
        public double LowestPrice { get; private set; }

        public ProductBasicInfoDTO(long id, string name, string description, string category, double lowestPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            Category = category;
            LowestPrice = lowestPrice;
        }
    }
}
