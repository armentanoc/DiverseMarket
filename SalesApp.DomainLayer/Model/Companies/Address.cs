using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Model.Companies
{
    internal class Address
    {

        public int ID {  get; }
        public string ZipCode { get; private set; }
        public string Street { get; private set; }
        public string? Complement { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public Address(string zipCode, string street, string neighborhood, string city, string state, string? complement = null)
        {
            ID = GenerateID();
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        private int GenerateID()
        {
            return Math.Abs(DateTime.Now.GetHashCode());
        }

        // Metódos (?)
        //private void SetZipCode(string zipCode){}
        //private void SetComplement(string complement){}
    }
}
