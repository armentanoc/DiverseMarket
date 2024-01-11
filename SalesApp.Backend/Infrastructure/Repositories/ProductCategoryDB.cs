using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Backend.Infrastructure.Repositories
{
    internal class ProductCategoryDB
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS ProductCategory (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                name VARCHAR(45) NOT NULL
            ); insert into ProductCategory (name) values ('roupa');";

        }
    }
}
