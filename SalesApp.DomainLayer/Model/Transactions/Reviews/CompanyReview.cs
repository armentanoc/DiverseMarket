using SalesApp.DomainLayer.Model.Users;
using SalesApp.DomainLayer.Model.Companies;
using SalesApp.Infrastructure.Operations;

namespace SalesApp.DomainLayer.Model.Transactions.Reviews
{
    internal class CompanyReview : Review
    {

        public Company Company { get; private set; }
        //public Customer Customer { get; private set; }
        /*public CompanyReview(ReviewEnum reviewEnum, Company company, Customer customer, string? comment = null) : base(reviewEnum, comment)
        {
            Company = company;
            Customer = customer;
        }*/
        public CompanyReview(ReviewEnum reviewEnum, string? comment = null) : base(reviewEnum, comment)
        {
        }
    }
}
