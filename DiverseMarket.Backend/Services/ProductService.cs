using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model.Products;

namespace DiverseMarket.Backend.Services
{
    public static class ProductService
    {
        public static List<ProductBasicInfoDTO> GetAllProducstBasicInfo()
        {
            List<Model.Product> products = ProductDB.GetAllProducst();

            List<ProductBasicInfoDTO> productBasicInfoDTOs = new List<ProductBasicInfoDTO>();

            foreach (var product in products)
            {
                double lowestPrice = ProductOfferDB.GetLowestPriceByProductId(product.Id);

                productBasicInfoDTOs.Add(new ProductBasicInfoDTO(product.Id, product.Name,
                    product.Description, product.Category, lowestPrice));

            }

            return productBasicInfoDTOs;
        }

        public static List<ProductOfferCompleteInfoDTO> GetAllProductOfferInfo(List<ProductOfferBasicInfoDTO> productOfferBasicInfoDTOs)
        {
            return ProductOfferDB.GetAllProductOfferInformation(productOfferBasicInfoDTOs);
        }

        public static List<ProductOfferBasicInfoDTO> GetAllProductOffersByCompanyUserId(long userId)
        {
            List<ProductOffer> productOffers = ProductOfferDB.GetAllCompanyProductOffers(userId);
            
            List<ProductOfferBasicInfoDTO> productOfferBasicInfoDTOs = new();
            
            foreach (var productOffer in productOffers)
            {
                productOfferBasicInfoDTOs.Add(
                    new ProductOfferBasicInfoDTO(
                        productOffer.Id,
                        productOffer.SellerId,
                        productOffer.ProductId,
                        productOffer.Price,
                        productOffer.Quantity
                        )
                    );
            }

            return productOfferBasicInfoDTOs;
        }

        public static bool UpdateProductOfferByCompleteInfoDTO(ProductOfferCompleteInfoDTO newProductOffer)
        {
            return ProductOfferDB.UpdateProductOffer(newProductOffer);
        }
    }
}
