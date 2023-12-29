using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    internal class UserDB
    {
        internal static string InitializeTable()
        {
            return @"
                    CREATE TABLE IF NOT EXISTS User (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name VARCHAR(45) NOT NULL,
                        username VARCHAR(45) NOT NULL,
                        password VARCHAR(45) NOT NULL,
                        email VARCHAR(45),
                        telephone INTEGER,
                        role TEXT NOT NULL CHECK(role IN ('Seller', 'Client', 'Moderator')),
                        Address_id INTEGER,
                        FOREIGN KEY (Address_id) REFERENCES Address(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );";
        }
    }
}
