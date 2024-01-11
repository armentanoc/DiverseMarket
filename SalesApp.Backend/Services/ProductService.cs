using SalesApp.Backend.DTOs;
using SalesApp.Backend.Infrastructure.Repositories;
using SalesApp.Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Backend.Services
{
    public static class ProductService
    {
        public static List<ProductBasicInfoDTO> GetAllProducstBasicInfo()
        {
            List<Product> products = ProductDB.GetAllProducst();

            List<ProductBasicInfoDTO> productBasicInfoDTOs = new List<ProductBasicInfoDTO>();

            foreach (var product in products)
            {
                double lowestPrice = ProductOfferDB.GetLowestPriceByProductId(product.Id);

                productBasicInfoDTOs.Add(new ProductBasicInfoDTO(product.Id, product.Name,
                    product.Description, product.Category, lowestPrice));

            }

            return productBasicInfoDTOs;
        }
    }
}
