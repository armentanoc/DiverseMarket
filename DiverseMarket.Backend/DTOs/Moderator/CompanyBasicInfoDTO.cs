using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.DTOs.Moderator
{
    public class CompanyBasicInfoDTO
    {
        public long Id { get; private set; }
        public string CNPJ { get; private set; }
        public string CorporateName { get; private set; }
        public string TradeName { get; private set; }

        public CompanyBasicInfoDTO(long id, string cnpj, string corporateName, string tradeName)
        {
            Id = id;
            CNPJ = cnpj;
            CorporateName = corporateName;
            TradeName = tradeName;
        }
    }
}
