using SalesApp.DomainLayer.DTOs;
using SalesApp.DomainLayer.Model.Transactions.Reviews;
using SalesApp.DomainLayer.Service;

namespace SalesApp.DomainLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Infrastructure.Program.Main(args);

            CompanyReview companyReview = CompanyReviewService.FakeCompanyReviewData();
           
            int clientId = companyReview.Customer.Id;
            int companyId = companyReview.Company.Id;
            string review = companyReview.ReviewEnum.ToString();
            string comment = companyReview.Comment;

            CompanyReviewDTO companyReviewDTO = CompanyReviewService.GetCompanyReviewDTO(clientId, companyId, review, comment);
            CompanyReviewService.AddCompanyReview(companyReviewDTO);
        }
    }
}
