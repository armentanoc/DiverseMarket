using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Companies;
using DiverseMarket.Logger;
using System.Data.SQLite;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class CompanyDB : DatabaseConnection
    {
        internal static string GetCompanyNameById(long id)
        {
            string name = "";
            try
            {
                Open();
                string query = @"SELECT trade_name
                     FROM Company 
                     where = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", id);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                    name = reader["name"].ToString();

                return name;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return name;

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
                new LogMessage(ex);
                return false;

            }
            finally { 
                Close(); 
            }
        }
    }
}
