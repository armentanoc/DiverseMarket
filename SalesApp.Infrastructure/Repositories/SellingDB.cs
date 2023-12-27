using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    internal class SellingDB
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS Selling (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                date_sale DATE,
                amount DECIMAL(10,2),
                Customer_id INTEGER,
                Company_id INTEGER,
                FOREIGN KEY (Customer_id) REFERENCES Customer(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
                FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );";

        }
    }
}
