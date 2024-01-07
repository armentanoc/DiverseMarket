using SalesApp.DomainLayer.DTOs;
using SalesApp.Infrastructure.Model;
using SalesApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Service
{
    public static class ProductService
    {
        public static List<ProductBasicInfoDTO> GetAllProducstBasicInfo()
        {
            List<Product> products = ProductDB.GetAllProducst();

            List<ProductBasicInfoDTO> productBasicInfoDTOs = new List<ProductBasicInfoDTO>();

            foreach(var product in products)
            {
                double lowestPrice = ProductOfferDB.GetLowestPriceByProductId(product.Id);

                productBasicInfoDTOs.Add(new ProductBasicInfoDTO(product.Id, product.Name,
                    product.Description, product.Category, lowestPrice));

            }

            return productBasicInfoDTOs;
        }
    }
}
