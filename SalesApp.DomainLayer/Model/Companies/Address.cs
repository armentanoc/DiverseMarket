using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesApp.DomainLayer.Model.Companies
{
    internal class Address
    {

        public int ID {  get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Address(string zipCode, string street, string? complement, string neighborhood, string city, string state)
        {
            ID = GerarID();
            ZipCode = zipCode;
            Street = street;
            Complement = complement;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        public int GerarID()
        {
            return Math.Abs(DateTime.Now.GetHashCode());
        }


        
    }
}
