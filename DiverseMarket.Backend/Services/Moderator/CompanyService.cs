using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.DTOs.Moderator;
using DiverseMarket.Backend.Infrastructure.Repositories;
using DiverseMarket.Backend.Model.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Services.Moderator
{
    public class CompanyService
    {
        public static List<CompanyBasicInfoDTO> GetAllCompaniesBasicInfo()
        {
            List<Company> companies = CompanyDB.GetAllCompanies();

            List<CompanyBasicInfoDTO> companiesBasicInfoDTOs = new List<CompanyBasicInfoDTO>();

            foreach (var company in companies)
            {
                companiesBasicInfoDTOs.Add(new CompanyBasicInfoDTO(company.Id, company.Cnpj, company.CorporateName,
                    company.TradeName));

            }

            return companiesBasicInfoDTOs;
        }
    }
}
