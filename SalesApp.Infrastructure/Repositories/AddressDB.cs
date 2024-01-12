using SalesApp.Infrastructure.Model;
using SalesApp.Infrastructure.Operations;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SalesApp.Infrastructure.Repositories
{
    public class AddressDB : DatabaseConnection
    {
        internal static string InitializeTable()
        {
            return @"
                    CREATE TABLE IF NOT EXISTS Address (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        User_id INTEGER NOT NULL UNIQUE,
                        street VARCHAR(45) NOT NULL,
                        number VARCHAR(10) NOT NULL,
                        complement VARCHAR(45),
                        zipcode VARCHAR(45) NOT NULL,
                        city VARCHAR(45) NOT NULL,
                        FOREIGN KEY (User_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );";
        }


        public static long RegisterAddress(long userId, string cep, string street, string? complement, string number, string city)
        {
            long id = 0;
            try
            {
                Open();
                string query = @"INSERT INTO Address (User_id, street, number, complement, zipcode, city) 
                         VALUES (@userId, @street, @number, @complement, @zipcode, @city);";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@userId", userId);
                _command.Parameters.AddWithValue("@street", street);
                _command.Parameters.AddWithValue("@number", number);
                _command.Parameters.AddWithValue("@complement", (object)complement ?? DBNull.Value);
                _command.Parameters.AddWithValue("@zipcode", cep);
                _command.Parameters.AddWithValue("@city", city);
                _command.ExecuteNonQuery();

                id = _connection.LastInsertRowId;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                id = -1;
            }
            finally
            {
                Close();
            }
            return id;
        }

        public static Address GetAddressByUserId(long userId)
        {
            Address address = null;
            try
            {
                Open();
                string query = "SELECT * FROM Address WHERE User_id = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", userId);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    address = new Address((long)reader["id"], reader["zipcode"].ToString(),
                        reader["street"].ToString(), reader["city"].ToString(), reader["number"].ToString(), reader["complement"].ToString());
                }

                return address;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return null;

            }
            finally { Close(); }
        }
    }
}
