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
                Company_id INTEGER,
                Product_id INTEGER,
                price DECIMAL(10,2),
                quantity UNSIGNED INTEGER
            );";

        }
    }
}
