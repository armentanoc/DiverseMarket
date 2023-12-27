using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    internal class ProductDB
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS Product (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                name VARCHAR(45) NOT NULL,
                description VARCHAR(45) NULL
            );";

        }
    }
}
