
using SalesApp.DomainLayer.Model.Transactions.Reviews;
using SalesApp.Infrastructure.Operations;

namespace SalesApp.DomainLayer.DTOs
{
    public class CompanyReviewDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; } 
        public int CompanyId { get; set; } 
        public string Review { get; set; }
        public string? Comment { get; set; }
        public CompanyReviewDTO(CompanyReview companyReview)
        {
            ClientId = companyReview.Customer.Id;
            CompanyId = companyReview.Company.Id;
            Review = companyReview.ReviewEnum.ToString();
            Comment = companyReview.Comment;
        }

        public void Create() => SaveCompanyReview.Execute(ClientId, CompanyId, Review, Comment);
    }
}
