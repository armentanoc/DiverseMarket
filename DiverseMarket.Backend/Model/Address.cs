
namespace DiverseMarket.Backend.Model
{
    public class Address
    {
        public long Id { get; }
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string? Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string Number { get; private set; }
        public Address(string zipCode, string street, string number, string neighborhood, string city, string? complement = null)
        {
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            Number = number;
        }

        public Address(long id, string zipCode, string street, string neighborhood, string city, string number, string? complement = null)
        {
            Id = id;
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            Number = number;
        }

    }
}
