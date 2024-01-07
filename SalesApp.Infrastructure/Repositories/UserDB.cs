using SalesApp.Infrastructure.Model;
using SalesApp.Infrastructure.Model.Enums;
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
using System.Xml.Linq;

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
                        role TEXT NOT NULL CHECK(role IN ('Seller', 'Client', 'Moderator'))
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
                Open();

                (string password, string salt) obj = HashUtil.GetHashedAndSalt(password);

                string query = @"insert into User(name, username, password, email, telephone, role) 
                        values (@fullName, @username, '" + $"{obj.password}" + @"', @email, @telephone, 'Client');";

                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@fullName", fullName);
                _command.Parameters.AddWithValue("@username", username);
                _command.Parameters.AddWithValue("@email", email);
                _command.Parameters.AddWithValue("@telephone", (object)telephone ?? DBNull.Value);

                _command.ExecuteNonQuery();

                long id = _connection.LastInsertRowId;

                query = @"insert into User_Salt values (@id, '" + $"{obj.salt}" + @"')";

                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", id);

                bool registered = _command.ExecuteNonQuery() > 0;

                id = _connection.LastInsertRowId;

                AddressDB.RegisterAddress(id, cep, street, complement, number, city);

                CustomerDB.RegisterCustomer(id, CPF);

                return registered;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return false;

            }
            finally { Close(); }
        }

       

        public static string GetUserFullNameById(long userId)
        {
            string name = "";
            try
            {
                Open();
                string query = "SELECT name FROM User WHERE id = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", userId);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    name = reader["name"].ToString();
                }

                return name;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return name;

            }
            finally { Close(); }
        }

        public static User GetUserById(long id)
        {
            User user = null;
            try
            {
                Open();
                string query = "SELECT * FROM User WHERE id = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", id);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    string role = reader["role"].ToString();
                    Roles userRole = new Roles();

                    switch (role)
                    {
                        case "Client": userRole = Roles.Client; break;
                        case "Moderator": userRole = Roles.Moderator; break;
                        default: userRole = Roles.Company; break;
                    }

                    user = new User(id, reader["username"].ToString(), reader["name"].ToString(),
                        reader["email"].ToString(), "", reader["telephone"].ToString(), userRole);
                }

                return user;
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
