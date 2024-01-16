using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Infrastructure.Util;
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
