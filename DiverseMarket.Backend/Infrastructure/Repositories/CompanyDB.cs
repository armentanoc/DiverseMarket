using DiverseMarket.Backend.DTOs.Moderator;
using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Companies;
using System.Data.SQLite;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class CompanyDB : DatabaseConnection
    {
        public static List<Company> GetAllCompanies()
        {
            List<Company> companies = new List<Company>();
            try
            {
                Open();
                string query = @"SELECT * FROM Company c JOIN User u ON c.User_id = u.id;";
                _command = new SQLiteCommand(query, _connection);

                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    companies.Add(new Company((long)reader["id"], reader["cnpj"].ToString(),
                        reader["corporate_name"].ToString(), reader["trade_name"].ToString()));
                }

                return companies;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return companies;

            }
            finally { Close(); }
        }

        public static CompanyBasicInfoDTO GetCompanyById(long companyId)
        {
            try
            {
                Open();
                string query = @"
                                SELECT 
                                    c.id AS CompanyId, c.cnpj, c.corporate_name, c.trade_name,
                                    u.id AS UserId, u.name, u.username, u.password, u.email, u.telephone, u.role,
                                    a.id AS AddressId, a.street, a.number, a.complement, a.zipcode, a.neighborhood, a.city
                                FROM Company c
                                LEFT JOIN User u ON c.User_id = u.id
                                LEFT JOIN Address a ON u.id = a.User_id
                                WHERE c.id = @companyId;";
                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@companyId", companyId);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    return new CompanyBasicInfoDTO(
                        (long)reader["CompanyId"],
                        reader["cnpj"].ToString(),
                        reader["corporate_name"].ToString(),
                        reader["trade_name"].ToString(),
                        new UserDTO(
                            (long)reader["UserId"],
                            reader["name"].ToString(),
                            reader["username"].ToString(),
                            reader["password"].ToString(),
                            reader["email"].ToString(),
                            reader["telephone"].ToString(),
                            reader["role"].ToString()
                        )
                    // TODO: Descobrir o pq o AddresDTO não está funcionando
                    //,
                    //new AddressDTO(
                    //    (long)reader["AddressId"],
                    //    reader["zipcode"].ToString(),
                    //    reader["street"].ToString(),
                    //    reader["complement"].ToString(),
                    //    reader["neighborhood"].ToString(),
                    //    reader["city"].ToString(),
                    //    reader["state"].ToString(),
                    //    reader["number"].ToString()
                    //)
                    );
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return null;
            }
            finally { Close(); }
        }

        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS Company (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                cnpj VARCHAR(14),
                corporate_name VARCHAR(45),
                trade_name VARCHAR(45),
                User_id INTEGER,
                FOREIGN KEY (User_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );";

        }

        internal static bool RegisterCompany(long userId, Company company)
        {
            try
            {
                Open();

                string query = @"insert into Company(cnpj, corporate_name, trade_name, User_id) 
                        values (@cnpj, @corporateName, @tradeName, @userId);";

                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@cnpj", company.Cnpj);
                _command.Parameters.AddWithValue("@corporateName", company.CorporateName);
                _command.Parameters.AddWithValue("@tradeName", company.TradeName);
                _command.Parameters.AddWithValue("@userId", (object)userId ?? DBNull.Value);

                bool registered = _command.ExecuteNonQuery() > 0;

                return registered;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return false;

            }
            finally { Close(); }
        }
    }
}
