using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class WalletTransactionsDB
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS WalletTransactions (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                Customer_id INTEGER NOT NULL,
                amount DECIMAL(10,2),
                date_transaction DATETIME,
                type TEXT NOT NULL CHECK(type IN ('Debit', 'Credit')),
                FOREIGN KEY (Customer_id) REFERENCES Customer(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );";

        }
    }
}
