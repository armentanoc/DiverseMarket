using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Services
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

        internal static string GetProductDescriptionByProductOfferId(long productOfferId)
        {
            long productId = ProductOfferDB.GetProductIdByProductOfferId(productOfferId);

            return ProductDB.GetProductDescriptionById(productId);
        }

        internal static string GetProductNameByProductOfferId(long productOfferId)
        {
            long productId = ProductOfferDB.GetProductIdByProductOfferId(productOfferId);

            return ProductDB.GetProductNameById(productId);
        }
    }
}
