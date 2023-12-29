using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Operations
{
    internal class CustomerDB
    {
        internal static string InitializeTable()
        {
            return @"
                    CREATE TABLE IF NOT EXISTS Customer (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        User_id INTEGER NOT NULL,
                        cpf VARCHAR(45) NOT NULL,
                        FOREIGN KEY (User_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );";
        }
    }
}
