namespace DiverseMarket.Backend.DTOs
{
    public class AddressDTO
    {
        public long Id { get; private set; }
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string? Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Number { get; private set; }

        public AddressDTO(long id, string zipCode, string street, string? complement, string neighborhood, string city, string state, string number)
        {
            Id = id;
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Number = number;
        }

        public AddressDTO(string zipCode, string street, string? complement, string neighborhood, string city, string number)
        {
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            Number = number;
        }
    }
}
