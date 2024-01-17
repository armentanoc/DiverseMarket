using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Enums;
using DiverseMarket.Backend.Model.Products;
using System.Data.SQLite;
using System.Reflection.PortableExecutable;

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

        public static double GetLowestPriceByProductId(long producId)
        {
            double price = 0;
            try
            {
                Open();
                string query = @"SELECT MIN(price) AS lowest_price
                                FROM ProductOffer
                                WHERE Product_id = @ProductId;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@ProductId", producId);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    price = (double)reader[0];
                }
                reader.Close();
                return price;
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error("An error occured in GetLowestPriceByProductId: " + ex.Message + ex.StackTrace);
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

                string query = @"SELECT *
                                FROM ProductOffer
                                WHERE Company_id = @CompanyId;";

                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@CompanyId", userId);

                var reader = _command.ExecuteReader();

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
                    reader.Close();
                    productOffers.Add(productOffer);
                }
                return productOffers;
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error("An error occured in GetAllCompanyProductOffers: " + ex.Message + ex.StackTrace);
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

                string query = @"INSERT INTO ProductOffer(Company_id, Product_id, price, quantity)
                VALUES(2, 1, 99, 5);";

                _command = new SQLiteCommand(query, _connection);

                bool registered = _command.ExecuteNonQuery() > 0;

                return registered;
            }
            catch (Exception ex)
            {
                MyLogger.Log.Error("An error occured in RegisterDefaultProductOffer: " + ex.Message + ex.StackTrace);
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

            string formattedIdList =
                string
                .Join(", ", productOfferBasicInfoDTOs
                .Select(productOffer => productOffer.ProductId));

            List<ProductOfferCompleteInfoDTO> productOffers = new();
            try
            {

                Open();
                string query = @"SELECT *
                                FROM Product
                                WHERE id IN (@formattedIdList);";

                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue
                    (
                    "@formattedIdList",
                    formattedIdList
                    );

                var reader = _command.ExecuteReader();

                while (reader.Read())
                {

                    long thisOfferProductId = Convert.ToInt64(reader["id"]);

                    ProductOfferBasicInfoDTO thisOfferBasicInfo =
                        productOfferBasicInfoDTOs.FirstOrDefault(p => p.ProductId == thisOfferProductId);

                    if (thisOfferBasicInfo != null)
                    {

                        ProductOfferCompleteInfoDTO productOfferCompleteDTO = new
                        (
                            id: thisOfferBasicInfo.Id,
                            companyId: thisOfferBasicInfo.CompanyId,
                            productId: thisOfferBasicInfo.ProductId,
                            price: thisOfferBasicInfo.Price,
                            quantity: thisOfferBasicInfo.Quantity,
                            name: reader["name"].ToString(),
                            description: reader["description"].ToString(),
                            category:
                                Enum.Parse<ProductCategory>(reader["ProductCategory_id"]
                                .ToString())
                                .ToString()
                        );

                        productOffers.Add(productOfferCompleteDTO);
                    }
                }

                reader.Close();
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

                string query = @"UPDATE ProductOffer
                        SET price = @Price,
                            quantity = @Quantity
                        WHERE id = @Id;";

                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@Price", newProductOffer.Price);
                _command.Parameters.AddWithValue("@Quantity", newProductOffer.Quantity);
                _command.Parameters.AddWithValue("@Id", newProductOffer.Id);

                bool updated = _command.ExecuteNonQuery() > 0;

                return updated;
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