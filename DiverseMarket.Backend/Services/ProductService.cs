using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Products;
using DiverseMarket.Logger;

namespace DiverseMarket.Backend.Services
{
    public static class ProductService
    {
        public static List<ProductBasicInfoDTO> GetAllProducstBasicInfo()
        {
            List<Model.Product> products = ProductDB.GetAllProducts();

            List<ProductBasicInfoDTO> productBasicInfoDTOs = new List<ProductBasicInfoDTO>();

            foreach (var product in products)
            {
                double lowestPrice = ProductOfferDB.GetLowestPriceByProductId(product.Id);

                productBasicInfoDTOs.Add(new ProductBasicInfoDTO(product.Id, product.Name,
                    product.Description, product.Category, lowestPrice));

            }

            return productBasicInfoDTOs;
        }

        public static List<ProductOfferCompleteInfoDTO> GetAllProductOfferInfo(long userId)
        {

            List<ProductOfferBasicInfoDTO> productOfferData =
                GetAllProductOffersByCompanyUserId(userId);
            
            new LogMessage($"Count productOfferData: {productOfferData.Count}");

            return ProductOfferDB.GetAllProductOfferInformation(productOfferData);
            //returns data with both ProductOffer and Product tables
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

        public static List<string> GetAllProductCategories()
        {
            return ProductCategoryDB.GetAllCategories();
        }

        public static bool InsertNewProductWithProductOffer(ProductOfferInsertDTO productOfferInsertDTO)
        {
            if (productOfferInsertDTO.Category.Equals(ProductCategory.Roupa.ToString()))
            {
                Model.Product product = new Model.Product(productOfferInsertDTO.Name, productOfferInsertDTO.Description, (long)ProductCategory.Roupa);
                int productId = ProductDB.InsertProduct(product);
                ProductOfferBasicInfoDTO productOfferDTO = new ProductOfferBasicInfoDTO(productOfferInsertDTO.CompanyUserId, productId, productOfferInsertDTO.Price, productOfferInsertDTO.Quantity);
                return ProductOfferDB.InsertProductOffer(productOfferDTO);
            }
            else if (productOfferInsertDTO.Category.Equals(ProductCategory.Calcados.ToString()))
            {
                Model.Product product = new Model.Product(productOfferInsertDTO.Name, productOfferInsertDTO.Description, (long)ProductCategory.Calcados);
                int productId = ProductDB.InsertProduct(product);
                ProductOfferBasicInfoDTO productOfferDTO = new ProductOfferBasicInfoDTO(productOfferInsertDTO.CompanyUserId, productId, productOfferInsertDTO.Price, productOfferInsertDTO.Quantity);
                return ProductOfferDB.InsertProductOffer(productOfferDTO);
            }
            else if (productOfferInsertDTO.Category.Equals(ProductCategory.Eletronicos.ToString()))
            {
                Model.Product product = new Model.Product(productOfferInsertDTO.Name, productOfferInsertDTO.Description, (long)ProductCategory.Eletronicos);
                int productId = ProductDB.InsertProduct(product);
                ProductOfferBasicInfoDTO productOfferDTO = new ProductOfferBasicInfoDTO(productOfferInsertDTO.CompanyUserId, productId, productOfferInsertDTO.Price, productOfferInsertDTO.Quantity);
                return ProductOfferDB.InsertProductOffer(productOfferDTO);
            }
            else if (productOfferInsertDTO.Category.Equals(ProductCategory.Livros.ToString()))
            {
                Model.Product product = new Model.Product(productOfferInsertDTO.Name, productOfferInsertDTO.Description, (long)ProductCategory.Livros);
                int productId = ProductDB.InsertProduct(product);
                ProductOfferBasicInfoDTO productOfferDTO = new ProductOfferBasicInfoDTO(productOfferInsertDTO.CompanyUserId, productId, productOfferInsertDTO.Price, productOfferInsertDTO.Quantity);
                return ProductOfferDB.InsertProductOffer(productOfferDTO);
            }
            else if (productOfferInsertDTO.Category.Equals(ProductCategory.Joias.ToString()))
            {
                Model.Product product = new Model.Product(productOfferInsertDTO.Name, productOfferInsertDTO.Description, (long)ProductCategory.Joias);
                int productId = ProductDB.InsertProduct(product);
                ProductOfferBasicInfoDTO productOfferDTO = new ProductOfferBasicInfoDTO(productOfferInsertDTO.CompanyUserId, productId, productOfferInsertDTO.Price, productOfferInsertDTO.Quantity);
                return ProductOfferDB.InsertProductOffer(productOfferDTO);
            }
            else
            {
                return false;
            }

 
        }

       /* public static bool InsertNewProductWithProductOffer(ProductOfferInsertDTO productOfferInsertDTO)
        {
            Model.Product product = new Model.Product(productOfferInsertDTO.Name, productOfferInsertDTO.Description, productOfferInsertDTO.CategoryId);
            int productId = ProductDB.InsertProduct(product);
            
            ProductOfferBasicInfoDTO productOfferDTO = new ProductOfferBasicInfoDTO(productOfferInsertDTO.CompanyUserId, productId, productOfferInsertDTO.Price, productOfferInsertDTO.Quantity);
            return ProductOfferDB.InsertProductOffer(productOfferDTO);

        }*/

        public static bool InsertNewProductOffer(ProductOfferBasicInfoDTO productOffer)
        {
            return ProductOfferDB.InsertProductOffer(productOffer);

        }

       

        public static bool UpdateProductOfferByCompleteInfoDTO(ProductOfferCompleteInfoDTO newProductOffer)
        {
            return ProductOfferDB.UpdateProductOffer(newProductOffer);
        }

        public static bool DeleteCompanyProductOfferByCompleteInfoDTO(ProductOfferCompleteInfoDTO productOfferData)
        {
            return ProductOfferDB.DeleteCompanyProductOffer(productOfferData); ;
        }


    }
}
