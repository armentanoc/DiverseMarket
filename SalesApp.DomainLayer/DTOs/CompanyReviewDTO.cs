
using SalesApp.DomainLayer.Model.Transactions.Reviews;
using SalesApp.Infrastructure.Operations;

namespace SalesApp.DomainLayer.DTOs
{
    public class CompanyReviewDTO
    {
        public int Id { get; private set; }
        public int ClientId { get; private set; } 
        public int CompanyId { get; private set; } 
        public string Review { get; private set; }
        public string? Comment { get; private set; }
        public CompanyReviewDTO(int clientId, int companyId, string review, string? comment)
        {
            ClientId = clientId;
            CompanyId = companyId;
            Review = review;
            Comment = comment;
        }
    }
}
