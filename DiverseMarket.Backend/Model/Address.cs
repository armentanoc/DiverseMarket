using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiverseMarket.Backend.Model
{
    public class Address
    {
        public long Id { get; }
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string? Complement { get; private set; }
        public string City { get; private set; }
        public string Number { get; private set; }
        public Address(string zipCode, string street, string city, string number, string? complement = null)
        {
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            City = city;
            Number = number;
        }

        public Address(long id, string zipCode, string street, string city, string number, string? complement = null)
        {
            Id = id;
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            City = city;
            Number = number;
        }

    }
}
