using SalesApp.Infrastructure.Operations;
using SalesApp.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
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
            (string password, string salt) obj = HashUtil.GetHashedAndSalt("vitoria123");
            
                string query = @"insert into Address(street, number, complement,
                    zipcode, neighborhood, city, state) values('rua jac', '394', 'apto 11', '13560000', 'Jardim Luftalla', 'São Carlos', 'São Paulo');"
                    +
                     @"insert into User(name, username, password, email, telephone, role, Address_id) 
                values ('vitória', 'vitorialira', '" + $"{obj.password}" + @"', 'vitoria@email.com', '55', 'Client', 1);"+
                     @"insert into User_Salt values (1, '"+ $"{obj.salt}" +@"');";
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
                    );" + query;
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

    }
}
