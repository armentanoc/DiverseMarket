using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Model.Users
{
    public class Address
    {
        public int Id { get; }
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string? Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Number { get; private set; }
        public Address(string zipCode, string street, string neighborhood, string city, string state, string number, string? complement = null)
        {
            Id = GenerateID();
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            Number = number;
        }

        private int GenerateID()
        {
            return Math.Abs(Guid.NewGuid().GetHashCode());
        }

        // Metódos (?)
        //private void SetZipCode(string zipCode){}
        //private void SetComplement(string complement){}
    }
}
