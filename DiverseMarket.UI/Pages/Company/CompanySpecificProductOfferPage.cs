using DiverseMarket.Backend.DTOs;

namespace DiverseMarket.UI.Pages.Company
{
    public partial class CompanySpecificProductOfferPage : Form
    {
        public CompanySpecificProductOfferPage(ProductOfferCompleteInfoDTO completeOfferInfo, long userId)
        {
            InitializeComponent(completeOfferInfo, userId);
        }
    }
}
