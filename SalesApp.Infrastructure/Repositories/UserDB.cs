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
                        role TEXT NOT NULL CHECK(role IN ('Seller', 'Client', 'Moderator')),
                        Address_id INTEGER NOT NULL,
                        FOREIGN KEY (Address_id) REFERENCES Address(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );";
        }
    }
}
