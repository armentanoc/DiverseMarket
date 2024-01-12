using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Infrastructure.Repositories
{
    internal class CompanyDB
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS Company (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                cnpj VARCHAR(14),
                corporate_name VARCHAR(45),
                trade_name VARCHAR(45),
                User_id INTEGER,
                FOREIGN KEY (User_id) REFERENCES User(id) ON DELETE NO ACTION ON UPDATE NO ACTION
            );";

        }
    }
}
