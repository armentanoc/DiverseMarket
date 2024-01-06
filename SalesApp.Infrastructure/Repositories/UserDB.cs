using SalesApp.Infrastructure.Operations;
using SalesApp.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    public class UserDB : DatabaseConnection
    {
        internal static string InitializeTable()
        {            
            return @"
                    CREATE TABLE IF NOT EXISTS User (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name VARCHAR(45) NOT NULL,
                        username VARCHAR(45) NOT NULL,
                        password VARCHAR(45) NOT NULL,
                        email VARCHAR(60),
                        telephone CHAR(11),
                        role TEXT NOT NULL CHECK(role IN ('Seller', 'Client', 'Moderator')),
                        Address_id INTEGER,
                        FOREIGN KEY (Address_id) REFERENCES Address(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );" +
                    @"
                    CREATE TABLE IF NOT EXISTS User_Salt (
                        User_id INTEGER NOT NULL PRIMARY KEY,
                        salt VARCHAR(45) NOT NULL,
                        FOREIGN KEY (User_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );";
        }

        public static (long? id, string? userRole) Login(string username, string password)
        {
            try
            {
                Open();

                string query = @"SELECT s.salt FROM User_Salt s INNER JOIN User u ON u.id = s.User_id where u.username=@username;";
                _command = new SQLiteCommand(query, _connection);
                _command.Parameters.AddWithValue("@username", username);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    string salt = reader["salt"].ToString();
                    string hashedPassword = HashUtil.GetHashedWithGivenSalt(password, salt);
                    if (hashedPassword == "")
                        return (null, null);

                    query = @"SELECT id, role
                     FROM User 
                     WHERE username=@username AND password=@password;";
                    _command = new SQLiteCommand(query, _connection);
                    _command.Parameters.AddWithValue("@username", username);
                    _command.Parameters.AddWithValue("@password", hashedPassword);
                    

                    reader = _command.ExecuteReader();

                    if (reader.Read())
                    {
                        long id = (long)reader["id"];
                        string role = reader["role"].ToString();
                        return (id, role);
                    }
                }
                return (null, null);
            }
            catch (SQLiteException e)
            {
                Console.WriteLine($"Erro ao abrir o banco de dados: {e.Message}\n");
                return (null, null);
            }
            finally
            {
                Close();
            }
        }

        public static long GetUserIdByUsername(string username)
        {
            long id = 0;
            try
            {
                Open();
                string query = "SELECT id FROM User WHERE username = @username;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@username", username);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    id = (long)(reader["id"]);
                }

                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return id;

            }
            finally { Close(); }
        }

        public static bool RegisterCustomer(string fullName, string email, string username, string? telephone, string CPF, 
           string cep,
                    string street,
                    string? complement, 
                    string number, string city, string password)
        {
            try
            {
                long addressId = RegisterAddress(cep, street, complement, number, city);
                if (addressId > 0)
                {

                    Open();

                    (string password, string salt) obj = HashUtil.GetHashedAndSalt(password);

                    string query = @"insert into User(name, username, password, email, telephone, role, Address_id) 
                        values (@fullName, @username, '" + $"{obj.password}" + @"', @email, @telephone, 'Client', @addressId);";

                    _command = new SQLiteCommand(query, _connection);

                    _command.Parameters.AddWithValue("@fullName", fullName);
                    _command.Parameters.AddWithValue("@username", username);
                    _command.Parameters.AddWithValue("@email", email);
                    _command.Parameters.AddWithValue("@telephone", (object)telephone ?? DBNull.Value);
                    _command.Parameters.AddWithValue("@addressId", addressId);

                    _command.ExecuteNonQuery();

                    long id = _connection.LastInsertRowId;

                    query = @"insert into User_Salt values (@id, '" + $"{obj.salt}" + @"')";

                    _command = new SQLiteCommand(query, _connection);

                    _command.Parameters.AddWithValue("@id", id);

                    return _command.ExecuteNonQuery() > 0;

                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return false;

            }
            finally { Close(); }
        }

        public static long RegisterAddress(string cep, string street, string? complement, string number, string city)
        {
            long id = 0;
            try
            {
                Open();
                string query = @"INSERT INTO Address (street, number, complement, zipcode, city) 
                         VALUES (@street, @number, @complement, @zipcode, @city);";
                _command = new SQLiteCommand(query, _connection);

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

    }
}
