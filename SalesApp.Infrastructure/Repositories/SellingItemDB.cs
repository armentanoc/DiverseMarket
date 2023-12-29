using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    internal class SellingItemDB
    {
        internal static string InitializeTable()
        {
            return @"CREATE TABLE IF NOT EXISTS SellingItem (
                            Selling_id INTEGER NOT NULL,
                            Product_id INTEGER NOT NULL,
                            quantity INTEGER NOT NULL,
                            price DECIMAL(10,2) NOT NULL,
                            Company_id INTEGER NOT NULL,
                            PRIMARY KEY (Selling_id, Product_id),
                            FOREIGN KEY (Selling_id) REFERENCES Selling (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION,
                            FOREIGN KEY (Product_id) REFERENCES Product (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION,
                            FOREIGN KEY (Company_id) REFERENCES Company (id)
                                ON DELETE NO ACTION
                                ON UPDATE NO ACTION
                        );";

        }


    }
}
