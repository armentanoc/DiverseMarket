using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    internal class AddressDB
    {
        internal static string InitializeTable()
        {
            return @"
                    CREATE TABLE IF NOT EXISTS Address (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        street VARCHAR(45) NOT NULL,
                        number VARCHAR(10) NOT NULL,
                        complement VARCHAR(45),
                        zipcode VARCHAR(45) NOT NULL,
                        city VARCHAR(45) NOT NULL
                    );";
        }
    }
}
