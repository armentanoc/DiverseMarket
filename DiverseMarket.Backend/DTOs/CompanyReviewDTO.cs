
namespace DiverseMarket.Backend.DTOs
{
    public class CompanyReviewDTO
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long CompanyId { get; set; }
        public string Review { get; set; }
        public string? Comment { get; set; }

        public CompanyReviewDTO(long clientId, long companyId, string review, string comment = "")
        {
           ClientId = clientId;
           CompanyId = companyId;
           Review = review;
           Comment = comment;
        }
    }
}