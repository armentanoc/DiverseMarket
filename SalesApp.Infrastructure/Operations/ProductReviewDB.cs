using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Operations
{
    public class ProductReviewDB
    {
        public static string CreateTable()
        {
            return "CREATE TABLE IF NOT EXISTS `ProductReview` (\r\n" +
        "    `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\r\n" +
        "    `review_enum` TEXT NOT NULL CHECK(review_enum IN ('Pessimo', 'Ruim', 'Regular', 'Otimo', 'Excelente')),\r\n" +
        "    `comment` VARCHAR(45),\r\n" +
        "    `SellingItem_Selling_id` INTEGER NOT NULL,\r\n" +
        "    `SellingItem_Product_id` INTEGER NOT NULL,\r\n" +
        "    FOREIGN KEY (`SellingItem_Selling_id`) REFERENCES `SellingItem` (`Selling_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,\r\n" +
        "    FOREIGN KEY (`SellingItem_Product_id`) REFERENCES `SellingItem` (`Product_id`) ON DELETE NO ACTION ON UPDATE NO ACTION\r\n" +
        ");";
        }
    }
}
