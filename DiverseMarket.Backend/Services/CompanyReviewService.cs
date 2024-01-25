using DiverseMarket.Backend.DTOs;
using DiverseMarket.Backend.Infrastructure.Repositories;

namespace DiverseMarket.Backend.Services
{
    public class CompanyReviewService
    {
        public static CompanyReviewDTO GetCompanyReviewDTO(int clientId, int companyId, string review, string? comment)
        {
            return new CompanyReviewDTO(clientId, companyId, review, comment);
        }

        public static void AddCompanyReview(CompanyReviewDTO companyReviewDTO)
        {
            ReviewCompanyDB.Insert(
                companyReviewDTO.ClientId,
                companyReviewDTO.CompanyId,
                companyReviewDTO.Review,
                companyReviewDTO.Comment
                );
        }

    }
}
