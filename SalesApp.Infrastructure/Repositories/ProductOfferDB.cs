using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    internal class ProductOfferDB
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS ProductOffer (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                Company_id INTEGER NOT NULL,
                Product_id INTEGER NOT NULL,
                price DECIMAL(10,2),
                quantity INTEGER,
                FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE SET NULL ON UPDATE CASCADE,
                FOREIGN KEY (Product_id) REFERENCES Product(id) ON DELETE SET NULL ON UPDATE CASCADE
            );
            ";

        }
    }
}
