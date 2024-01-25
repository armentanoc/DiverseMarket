using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiverseMarket.UI.Pages.Company
{
    public partial class AddSpecificProductOfferPage : Form
    {
        public AddSpecificProductOfferPage(long userId)
        {
            InitializeComponent(userId);
        }
    }
}
