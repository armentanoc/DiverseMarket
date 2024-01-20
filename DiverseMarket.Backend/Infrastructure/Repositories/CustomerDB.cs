using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Infrastructure.Repositories
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

        public static string InitializeTable()
        {
            return @"
                    CREATE TABLE IF NOT EXISTS Customer (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        User_id INTEGER NOT NULL,
                        cpf VARCHAR(45) NOT NULL,
                        FOREIGN KEY (User_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );";
        }

        public static bool RegisterCustomer(long userId, string cpf)
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

        //public static CustomerDTO GetCustomerByUserId(long userId)
        //{
        //    try
        //    {
        //        Open() ;

        //        string query = "SELECT * FROM Customer WHERE User_id = @userId";
        //        using (SQLiteCommand command = new SQLiteCommand(query, _connection))
        //        {
        //            command.Parameters.AddWithValue("@userId", userId);

        //            using (SQLiteDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    CustomerDTO customer = new CustomerDTO
        //                    {
        //                        Id = Convert.ToInt64(reader["id"]),
        //                        UserId = Convert.ToInt64(reader["User_id"]),
        //                        CPF = Convert.ToString(reader["cpf"])
        //                    };

        //                    return customer;
        //                }
        //            }
        //        }
        //    } catch (Exception ex)
        //    {
        //        Console.WriteLine("An error occured: " + ex.Message);
        //        return "";
        //    }

        //    finally { Close(); }

        //    return null;
        //}
    }
}
