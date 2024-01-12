using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class ProductReviewDB
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS ProductReview (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                review TEXT NOT NULL CHECK(review IN ('Pessimo', 'Ruim', 'Regular', 'Otimo', 'Excelente')),
                comment VARCHAR(45),
                SellingItem_Selling_id INTEGER NOT NULL,
                SellingItem_Product_id INTEGER NOT NULL,
                FOREIGN KEY (SellingItem_Selling_id) REFERENCES SellingItem(Selling_id) ON DELETE NO ACTION ON UPDATE NO ACTION,
                FOREIGN KEY (SellingItem_Product_id) REFERENCES SellingItem(Product_id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );";

        }
    }
}
