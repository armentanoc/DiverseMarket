using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.Infrastructure.Repositories
{
    internal class ReviewCompanyDB
    {
        internal static string InitializeTable()
        {
            return @"
            CREATE TABLE IF NOT EXISTS ReviewCompany (
                id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                Client_id INTEGER NOT NULL,
                Company_id INTEGER NOT NULL,
                review TEXT NOT NULL CHECK(review IN ('Pessimo', 'Ruim', 'Regular', 'Otimo', 'Excelente')),
                comment VARCHAR(150),
                FOREIGN KEY (Company_id) REFERENCES Company(id) ON DELETE SET NULL ON UPDATE CASCADE,
                FOREIGN KEY (Client_id) REFERENCES Client(id) ON DELETE SET NULL ON UPDATE CASCADE
            );";

        }
    }
}
