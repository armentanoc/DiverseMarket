using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model.Products;
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
                MyLogger.Log.Error("An error occurred in GetLowestPriceByProductId: " + ex.Message + ex.StackTrace);
                return price;
            }
            finally
            {
                Close();
            }
        }

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
                MyLogger.Log.Error("An error occurred in GetAllCompanyProductOffers: " + ex.Message + ex.StackTrace);
                return productOffers;
            }
            finally
            {
                Close();
            }
        }

        internal static bool RegisterDefaultProductOffer()
        {
            try
            {
                Open();
                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"INSERT INTO ProductOffer(Company_id, Product_id, price, quantity)
                                                    VALUES(2, 1, 99, 5);";

                    return command.ExecuteNonQuery() > 0;
                }

            }
            catch (Exception ex)
            {
                MyLogger.Log.Error("An error occurred in RegisterDefaultProductOffer: " + ex.Message + ex.StackTrace);
                return false;
            }
            finally
            {
                Close();
            }
        }

        internal static List<ProductOfferCompleteInfoDTO> GetAllProductOfferInformation(List<ProductOfferBasicInfoDTO> productOfferBasicInfoDTOs)
        {
            List<long> productIdList = productOfferBasicInfoDTOs
                .Select(productOffer => productOffer.ProductId)
                .ToList();

            string formattedIdList = string.Join(", ", productOfferBasicInfoDTOs.Select(productOffer => productOffer.ProductId));

            List<ProductOfferCompleteInfoDTO> productOffers = new();
            try
            {
                Open();
                using (var command = new SQLiteCommand(_connection))
                {
                    command.CommandText = @"SELECT *
                                                    FROM Product
                                                    WHERE id IN (@formattedIdList);";
                    command.Parameters.AddWithValue("@formattedIdList", formattedIdList);

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
                                    companyId: thisOfferBasicInfo.CompanyId,
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

                return productOffers;
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error("GetAllProductOfferInformation: " + ex.Message + ex.StackTrace);
                return productOffers;
            }
            finally
            {
                Close();
            }
        }

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
                MyLogger.Log.Error("An error occurred in UpdateProductOffer: " + ex.Message + ex.StackTrace);
                return false;
            }
            finally
            {
                Close();
            }
        }
    }
}
