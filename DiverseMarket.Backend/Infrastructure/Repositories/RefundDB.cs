using DiverseMarket.Backend.Infrastructure.Operations;
using DiverseMarket.Backend.Model.Transactions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class RefundDB : DatabaseConnection
    {
        internal static List<Refund> GetAllRefundsByUserId(long userId)
        {
            throw new NotImplementedException();
        }

        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS Refund (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Customer_id INTEGER NOT NULL,
                Product_id INTEGER NOT NULL,
                Company_id INTEGER NOT NULL,
                Status INTEGER NOT NULL CHECK(Status IN (1, 2, 3, 4)),
                TotalAmount REAL NOT NULL,
                CustomerComment TEXT,
                ModeratorComment TEXT,
                SellerComment TEXT,
                FOREIGN KEY (Customer_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
                FOREIGN KEY (Product_id) REFERENCES Product(id) ON DELETE NO ACTION ON UPDATE NO ACTION,
                FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );
            ";

        }
    }
}
