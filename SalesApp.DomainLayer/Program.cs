using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Model.Companies;
using SalesApp.DomainLayer.Model.Transactions.Reviews;
using SalesApp.DomainLayer.Model.Users;
using SalesApp.DomainLayer.Service;

namespace SalesApp.DomainLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Infrastructure.Program.Main(args);
            Test.TrySavingCompanyReview();
        }
    }
}
