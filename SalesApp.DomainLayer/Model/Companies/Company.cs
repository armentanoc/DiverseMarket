using SalesApp.DomainLayer.Model.Users;

namespace SalesApp.DomainLayer.Model.Companies
{
    public class Company : User
    {
        //- Cadastradas pelo moderador
        //- Lidas atraves de um json
        //- Podem ser atualizadas pelo moderador
        private string _cnpj;
        public string Cnpj
        {
            get
            {
                return _cnpj;
            }
            private set
            {
                if (IsCnpj(value))
                {
                    _cnpj = value;
                }
                else
                {
                    throw new ArgumentException("CNPJ inválido. Tente novamente!");
                }

            }
        }
        public string CorporateName { get; private set; }
        public string TradeName { get; private set; }
        public int? LegalEntityTypeCode { get; private set; }

        public Company(string username, string name, string email, string password, int phone, Address address, string cnpj, string corporateName, string tradeName, int? legalEntityTypeCode = null, RolesEnum role = RolesEnum.Company) : base (username, name, email, password, phone, role, address)
        {
            _cnpj = cnpj;
            CorporateName = corporateName;
            TradeName = tradeName;
            LegalEntityTypeCode = legalEntityTypeCode;
        }


        private static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            if (cnpj.Length != 14)
                return false;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }


        public void ChangeCorporateName(string corporateName)
        {
            CorporateName = corporateName;
        }

        public void ChangeTradeName(string tradeName)
        {
            TradeName = tradeName;
        }

        public void ChangeLegalEntityCode(int legalEntityCode)
        {
            LegalEntityTypeCode = legalEntityCode;
        }
    }
}
