using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.DTOs
{
    public class ProductBasicInfoDTO
    {
        public long Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Category { get; }
        public double LowestPrice { get; }

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
