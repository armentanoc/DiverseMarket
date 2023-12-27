using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Operations
{
    public class ProductDB
    {
        public static string CreateTable() 
        {
            return "CREATE TABLE IF NOT EXISTS `Product` (\r\n" +
            "    `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\r\n" +
            "    `name` VARCHAR(45) NOT NULL,\r\n" +
            "    `description` VARCHAR(45) NULL,\r\n" +
            ");";
        }
    }
}
