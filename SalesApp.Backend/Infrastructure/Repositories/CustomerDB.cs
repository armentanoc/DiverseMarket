using SalesApp.Backend.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Backend.Infrastructure.Repositories
{
    public class CustomerDB : DatabaseConnection
    {
        public static string GetCustomerCPFById(long id)
        {
            string CPF = "";
            try
            {
                Open();
                string query = "select cpf from Customer where User_id = @userId;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@userId", id);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    CPF = reader[0].ToString();
                }
                return CPF;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return "";

            }
            finally { Close(); }
        }

        internal static string InitializeTable()
        {
            return @"
                    CREATE TABLE IF NOT EXISTS Customer (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        User_id INTEGER NOT NULL,
                        cpf VARCHAR(45) NOT NULL,
                        FOREIGN KEY (User_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );";
        }

        internal static bool RegisterCustomer(long userId, string cpf)
        {
            try
            {
                Open();
                string query = "insert into Customer(User_id, cpf) values(@userId, @cpf);";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@userId", userId);
                _command.Parameters.AddWithValue("@cpf", cpf);

                return _command.ExecuteNonQuery() > 0;

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
