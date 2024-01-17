namespace DiverseMarket.Backend.DTOs.Moderator
{
    public class CompanyBasicInfoDTO
    {
        public long Id { get; private set; }
        public string CNPJ { get; private set; }
        public string CorporateName { get; private set; }
        public string TradeName { get; private set; }
        public UserDTO? User { get; set; }
        public AddressDTO? Address { get; set; }

        public CompanyBasicInfoDTO(long id, string cnpj, string corporateName, string tradeName, UserDTO user = null, AddressDTO address = null)
        {
            Id = id;
            CNPJ = cnpj;
            CorporateName = corporateName;
            TradeName = tradeName;
            User = user;
            Address = address;
        }
    }
}
