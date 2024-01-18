using DiverseMarket.Backend.DTOs;

namespace DiverseMarket.UI.Pages.Company
{
    public partial class CompanySpecificOfferPage : Form
    {
        public CompanySpecificOfferPage(ProductOfferCompleteInfoDTO completeOfferInfo, long userId)
        {
            InitializeComponent(completeOfferInfo, userId);
        }
    }
}
