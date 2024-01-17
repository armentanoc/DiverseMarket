using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model;
using DiverseMarket.Logger;
using System.Data.SQLite;

namespace DiverseMarket.Backend.Infrastructure.Repositories
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
                        neighborhood VARCHAR(45) NOT NULL,
                        city VARCHAR(45) NOT NULL,
                        FOREIGN KEY (User_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );";
        }


        public static long RegisterAddress(long userId, string cep, string street, string number, string? complement, string neighborhood, string city)
        {
            long id = 0;
            try
            {
                Open();
                string query = @"INSERT INTO Address (User_id, street, number, complement, zipcode, neighborhood, city) 
                         VALUES (@userId, @street, @number, @complement, @zipcode, @neighborhood, @city);";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@userId", userId);
                _command.Parameters.AddWithValue("@street", street);
                _command.Parameters.AddWithValue("@number", number);
                _command.Parameters.AddWithValue("@complement", (object)complement ?? DBNull.Value);
                _command.Parameters.AddWithValue("@zipcode", cep);
                _command.Parameters.AddWithValue("@neighborhood", neighborhood);
                _command.Parameters.AddWithValue("@city", city);
                _command.ExecuteNonQuery();

                id = _connection.LastInsertRowId;

            }
            catch (Exception ex)
            {
                new LogMessage(ex);
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
                new LogMessage(ex);
                return null;

            }
            finally { Close(); }
        }

        internal static bool UpdateAddressByUserId(long userId, AddressDTO address)
        {
            try
            {
                Open();
                string query = @"UPDATE Address
                        SET
                            street = @street,
                            number = @number,
                            complement = @complement,
                            zipcode = @zipcode,
                            neighborhood = @neighborhood,
                            city = @city
                        WHERE User_id = @userId";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@userId", userId);
                _command.Parameters.AddWithValue("@street", address.Street);
                _command.Parameters.AddWithValue("@number", address.Number);
                _command.Parameters.AddWithValue("@zipcode", address.ZipCode);
                _command.Parameters.AddWithValue("@city", address.City);
                _command.Parameters.AddWithValue("@neighborhood", address.Neighborhood);
                _command.Parameters.AddWithValue("@complement", (object)address.Complement ?? DBNull.Value);

                return _command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                new LogMessage(ex);
                return false;

            }
            finally { Close(); }
        }
    }
}
