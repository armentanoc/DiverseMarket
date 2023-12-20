namespace SalesApp.DomainLayer.Model.Companies
{
    internal class Company
    {

        //- Cadastradas pelo moderador
        //- Lidas atraves de um json
        //- Podem ser atualizadas pelo moderador
        public int ID { get; }
        public string CNPJ { get; private set; }
        public string CorporateName { get; private set; }
        public string TradeName { get; private set; }
        public int? LegalEntityTypeCode { get; private set; }
        public int? Telephone { get; private set; }
        public Address Address { get; private set; }

        public Company(string cnpj, string corporateName, string tradeName, Address address, int? legalEntityTypeCode = null, int? telephone = null)
        {
            ID = GenerateID();
            CNPJ = cnpj;
            CorporateName = corporateName;
            TradeName = tradeName;
            LegalEntityTypeCode = legalEntityTypeCode;
            Telephone = telephone;
            Address = address;
        }

        private int GenerateID()
        {
            return Math.Abs(DateTime.Now.GetHashCode());
        }

    }
}
