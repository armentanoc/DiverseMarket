﻿using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model;
using DiverseMarket.Backend.Model.Products;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DiverseMarket.Backend.Model.Products.Product;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    public class ProductDB : DatabaseConnection
    {
        internal static List<Product> GetAllProducst()
        {
            List<Product> products = new List<Product>();
            try
            {
                Open();
                string query = @"SELECT p.id, p.name, p.description, pc.name AS category_name
                     FROM Product p
                     JOIN ProductCategory pc ON p.ProductCategory_id = pc.id;";
                _command = new SQLiteCommand(query, _connection);

                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product((long)reader["id"], reader["name"].ToString(),
                        reader["description"].ToString(), reader["category_name"].ToString()));
                }

                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return products;

            }
            finally { Close(); }
        }

        internal static string GetProductDescriptionById(long productId)
        {
            string description = "";
            try
            {
                Open();
                string query = @"SELECT description
                     FROM Product 
                     where id = @id;";
                _command = new SQLiteCommand(query, _connection);

                _command.Parameters.AddWithValue("@id", productId);

                var reader = _command.ExecuteReader();

                if (reader.Read())
                    description = reader["description"].ToString();

                return description;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occured: " + ex.Message);
                return description;

            }
            finally { Close(); }
        }

        internal static string GetProductNameById(long id)
        {
            string name = "";
            try
            {
                Open();
                string query = @"SELECT name
                     FROM Product 
                     where id = @id;";
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
            CREATE TABLE IF NOT EXISTS Product (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                name VARCHAR(45) NOT NULL,
                description VARCHAR(45) NOT NULL,
                ProductCategory_id INTEGER NOT NULL,  
                FOREIGN KEY (ProductCategory_id) REFERENCES ProductCategory(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            ); insert into Product (name, description, ProductCategory_id) values ('Camiseta', 'camiseta nike', 1);";

        }
    }
}
