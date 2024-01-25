using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model.Products;
using DiverseMarket.Logger;
using System.Data.SQLite;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    public class ProductOfferDB : DatabaseConnection
    {
        public static string InitializeTable()
        {
            return @"
                CREATE TABLE IF NOT EXISTS ProductOffer (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    Company_id INTEGER NOT NULL,
                    Product_id INTEGER NOT NULL,
                    price DECIMAL(10,2),
                    quantity INTEGER,
                    FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE SET NULL ON UPDATE CASCADE,
                    FOREIGN KEY (Product_id) REFERENCES Product(id) ON DELETE SET NULL ON UPDATE CASCADE
                );
                ";
        }

        #region GetLowestPriceByProductId
        public static double GetLowestPriceByProductId(long productId)
        {
            double price = 0;
            try
            {
                Open();
                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"SELECT MIN(price) AS lowest_price
                                                    FROM ProductOffer
                                                    WHERE Product_id = @ProductId;";
                    command.Parameters.AddWithValue("@ProductId", productId);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            price = (double)reader[0];
                        }
                    }
                }
                return price;
            }
            catch (Exception ex)
            {
                new LogMessage("An error occurred in GetLowestPriceByProductId: " + ex.Message + ex.StackTrace);
                return price;
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region GetAllCompanyProductOffers
        internal static List<ProductOffer> GetAllCompanyProductOffers(long userId)
        {
            List<ProductOffer> productOffers = new();
            try
            {
                Open();
                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"SELECT *
                                                    FROM ProductOffer
                                                    WHERE Company_id = @CompanyId;";
                    command.Parameters.AddWithValue("@CompanyId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductOffer productOffer = new ProductOffer
                            (
                                id: Convert.ToInt32(reader["id"]),
                                sellerId: Convert.ToInt32(reader["Company_id"]),
                                productId: Convert.ToInt32(reader["Product_id"]),
                                price: Convert.ToDecimal(reader["price"]),
                                quantity: Convert.ToInt32(reader["quantity"])
                            );
                            productOffers.Add(productOffer);
                        }
                    }
                }

                return productOffers;
            }
            catch (Exception ex)
            {
                new LogMessage("An error occurred in GetAllCompanyProductOffers: " + ex.Message + ex.StackTrace);
                return productOffers;
            }
            finally
            {
                Close();
            }
        }
        #endregion

        #region RegisterDefaultProductOffer
        internal static bool InsertProductOffer(ProductOfferBasicInfoDTO productOffer)
        {
            try
            {
                Open();
                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"INSERT INTO ProductOffer(Company_id, Product_id, price, quantity)
                                    SELECT @CompanyUserId, @ProductId, @Price, @Quantity
                                    WHERE NOT EXISTS (
                                        SELECT 1 FROM ProductOffer 
                                        WHERE Product_id = @ProductId AND Company_id = @CompanyUserId
                                    )";

                    command.Parameters.AddWithValue("@CompanyUserId", productOffer.CompanyUserId);
                    command.Parameters.AddWithValue("@ProductId", productOffer.ProductId);
                    command.Parameters.AddWithValue("@Price", productOffer.Price);
                    command.Parameters.AddWithValue("@Quantity", productOffer.Quantity);

                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                new LogMessage(ex);
                return false;
            }
            finally
            {
                Close();
            }
        }

        public static void RegisterDefaultProductOffers()
        {
            try
            {
                ProductOfferBasicInfoDTO productOffer;

                //companyId, productId, price, quantity

                //roupas
                productOffer = new ProductOfferBasicInfoDTO(2, 1, 99, 5);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 2, 119, 6);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 3, 69, 2);
                InsertProductOffer(productOffer);

                //tênis
                productOffer = new ProductOfferBasicInfoDTO(2, 4, 389, 2);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 5, 419, 4);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 6, 299, 6);
                InsertProductOffer(productOffer);

                //eletrônicos
                productOffer = new ProductOfferBasicInfoDTO(2, 7, 5000, 7);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 8, 750, 8);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 9, 120, 9);
                InsertProductOffer(productOffer);

                //livros
                productOffer = new ProductOfferBasicInfoDTO(2, 10, 79, 1);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 11, 69, 2);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 12, 59, 4);
                InsertProductOffer(productOffer);

                //joias
                productOffer = new ProductOfferBasicInfoDTO(2, 13, 89, 4);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 14, 999, 5);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 15, 129, 7);
                InsertProductOffer(productOffer);

                //mais joias para testar autoscroll
                productOffer = new ProductOfferBasicInfoDTO(2, 16, 89, 4);
                InsertProductOffer(productOffer);
                productOffer = new ProductOfferBasicInfoDTO(2, 17, 79, 5);
                InsertProductOffer(productOffer);

            }
            catch (Exception ex)
            {
                new LogMessage(ex);
            }
        }
        #endregion

        #region GetAllProductOfferInformation
        internal static List<ProductOfferCompleteInfoDTO> GetAllProductOfferInformation(List<ProductOfferBasicInfoDTO> productOfferBasicInfoDTOs)
        {
            List<long> productIdList = productOfferBasicInfoDTOs
                .Select(productOffer => productOffer.ProductId)
                .ToList();

            List<ProductOfferCompleteInfoDTO> productOffers = new List<ProductOfferCompleteInfoDTO>();

            try
            {
                Open();

                string parameterNames = string.Join(", ", productIdList.Select((_, index) => "@productId" + index));

                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = $"SELECT * FROM Product WHERE id IN ({parameterNames})";

                    for (int i = 0; i < productIdList.Count; i++)
                    {
                        command.Parameters.AddWithValue($"@productId{i}", productIdList[i]);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long thisOfferProductId = Convert.ToInt64(reader["id"]);
                            ProductOfferBasicInfoDTO thisOfferBasicInfo = productOfferBasicInfoDTOs.FirstOrDefault(p => p.ProductId == thisOfferProductId);

                            if (thisOfferBasicInfo != null)
                            {
                                ProductOfferCompleteInfoDTO productOfferCompleteDTO = new ProductOfferCompleteInfoDTO
                                (
                                    id: thisOfferBasicInfo.Id,
                                    companyUserId: thisOfferBasicInfo.CompanyUserId,
                                    productId: thisOfferBasicInfo.ProductId,
                                    price: thisOfferBasicInfo.Price,
                                    quantity: thisOfferBasicInfo.Quantity,
                                    name: reader["name"].ToString(),
                                    description: reader["description"].ToString(),
                                    category: Enum.Parse<ProductCategory>(reader["ProductCategory_id"].ToString()).ToString()
                                );

                                productOffers.Add(productOfferCompleteDTO);
                            }
                        }
                    }
                }

                new LogMessage($"Count productOfferAllInformation: {productOffers.Count}");
                return productOffers;
            }
            catch (Exception ex)
            {
                new LogMessage("GetAllProductOfferInformation: " + ex.Message + ex.StackTrace);
                return productOffers;
            }
            finally
            {
                Close();
            }
        }

        #endregion

        #region UpdateProductOffer
        internal static bool UpdateProductOffer(ProductOfferCompleteInfoDTO newProductOffer)
        {
            try
            {
                Open();
                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"UPDATE ProductOffer
                                                    SET price = @Price,
                                                        quantity = @Quantity
                                                    WHERE id = @Id;";

                    command.Parameters.AddWithValue("@Price", newProductOffer.Price);
                    command.Parameters.AddWithValue("@Quantity", newProductOffer.Quantity);
                    command.Parameters.AddWithValue("@Id", newProductOffer.Id);

                    return command.ExecuteNonQuery() > 0;
                }

            }
            catch (Exception ex)
            {
                new LogMessage("An error occurred in UpdateProductOffer: " + ex.Message + ex.StackTrace);
                return false;
            }
            finally
            {
                Close();
            }
        }

        #endregion

        internal static bool DeleteCompanyProductOffer(ProductOfferCompleteInfoDTO productOffer)
        {
            try
            {
                Open();
                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"DELETE FROM ProductOffer WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", productOffer.Id);
                    return command.ExecuteNonQuery() > 0;
                }

            }
            catch (Exception ex)
            {
                new LogMessage(ex);
                return false;
            }
            finally
            {
                Close();
            }
        }
    }
}
